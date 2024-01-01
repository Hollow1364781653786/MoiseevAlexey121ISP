extends Enemy

# здесь сначала сделан код нахождения пути до противника

@onready var player = get_node("Player_Pathfinding")
@onready var shot = $EnemyDetect/Shot
@onready var timer = $Attack
@onready var knockbackTimer = $Knockback
@onready var right_wall = $RightWall #рейкаст для проверки стены справа
@onready var left_wall = $LeftWall #рейкаст для проверки стены слева
#@onready var raycastRight = $Right #рейкаст для осуществления прыжка вправо
#@onready var raycastLeft = $Left #рейкаст для осуществления прыжка влево
@onready var nav: NavigationAgent2D = $NavigationAgent2D #штука, осуществляющая нахождение пути
@onready var animated_sprite_2d = $AnimatedSprite2D 
@onready var enemy_detect = $EnemyDetect
@onready var enemy_healthbar = $enemy_healthbar
@onready var collision_shape_2d = $CollisionShape2D
@onready var stunTimer = $Stun
@onready var head_button = $HeadButton
@onready var torso_button = $TorsoButton
@onready var legs_button = $LegsButton
@onready var knockback_no_stun = $KnockbackNoStun

func _ready():
	nav.target_position = position
	enemy_healthbar.set_max_hearts(MAXIMUM_HEALTH)
	connect("health_changed", enemy_healthbar.update_hearts)
	enemy_healthbar.update_hearts(CURRENT_HEALTH)
	attackButtons = get_tree().get_nodes_in_group("Buttons")

#func _process(delta):
	#print(timer.time_left)

func _physics_process(delta):
	if not dying:
		if not stunned:
			if enemySeen and not shooting:
				wandering = false
				attack()
			else:
				wandering = true
				aiming = false
				wander(delta)
				if not timer.is_stopped():
					timer.stop()
			var _direction:= Vector2()
			_direction = nav.get_next_path_position() - global_position
			var direction_axis
			direction_axis = _direction.normalized()
			apply_gravity(delta)
			apply_friction(direction_axis, delta)
			apply_pathfinding(delta, direction_axis, _direction)
			update_animation(direction_axis, delta)
			move_and_slide()
			shoot(delta) # это функция непосредственного выстрела, она вызывается именно здесь потому что требует постоянного обновления
			scan_for_player()
			player = get_node("../Player_Pathfinding")
			player.connect("deal_damage_head", take_damage_head)
			player.connect("deal_damage_torso", take_damage_torso)
			player.connect("deal_damage_legs", take_damage_legs)
		else:
			animated_sprite_2d.play("hurt")
	else:
		for button in attackButtons:
			if button == null:
				attackButtons.erase(button)
			else:
				button._on_invisible()
		animated_sprite_2d.play("death")
		var currentFrame = animated_sprite_2d.frame
		var totalFrames = animated_sprite_2d.sprite_frames.get_frame_count("death")
		if currentFrame == totalFrames - 1:
			while get_child_count() > 0: # удаляем все дочерние узлы шоб не ломали код
				var child = get_child(0)
				remove_child(child)
				child.queue_free()
			queue_free()

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y += gravity * delta

#func perform_jump():
#	if is_on_floor():
#		velocity.y = JUMP_VELOCITY

func apply_friction(direction_axis, delta):
	if direction_axis.x == 0:
		velocity.x = move_toward(velocity.x, 0, FRICTION * delta)
	if abs(velocity.x) > SPEED - 20:
		FRICTION *= 5

func update_animation(direction_axis, delta):
	if not dying and CURRENT_HEALTH != 0:
		facingLeft = animated_sprite_2d.flip_h
		var current_position = position
		if abs(velocity.x) < 5 or current_position == previous_position:
			animated_sprite_2d.play("idle")
		elif abs(velocity.x) < 50.0:
			if not knockback:
				animated_sprite_2d.flip_h = direction_axis.x < 0
			animated_sprite_2d.play("walk")
		else:
			if not knockback:
				animated_sprite_2d.flip_h = direction_axis.x < 0
			animated_sprite_2d.play("running")
		if knockback:
			animated_sprite_2d.play("jumping")
		previous_position = current_position #эти две переменные нужны только для того чтобы проверять изменения позиции
	if shooting:
		animated_sprite_2d.play("shoot")
	if aiming: # если скоро выстрелит
		animated_sprite_2d.play("aim")

func apply_pathfinding(delta, direction_axis, _direction):
	nav.target_position.y = position.y - 15.0 #точка назначения фиксируется над персом чтобы он замедлялся
	velocity.x = move_toward(velocity.x, SPEED * direction_axis.x, ACCELERATION * delta) #движение к точке назначения
	if nav.target_position.x - position.x == 10.0:
		nav.target_position.x = position.x
	if not dying:
		if right_wall != null:
			if right_wall.is_colliding():
				if right_wall.get_collider().is_in_group("Player") and facingLeft:
					animated_sprite_2d.flip_h = false
			if left_wall.is_colliding():
				if left_wall.get_collider().is_in_group("Player") and not facingLeft:
					animated_sprite_2d.flip_h = true
#	if raycastRight.is_colliding() and nav.distance_to_target() > 20.0 and velocity.x > 0 and !right_wall.is_colliding():
#		perform_jump()
#	if raycastLeft.is_colliding() and nav.distance_to_target() > 20.0 and velocity.x < 0 and !left_wall.is_colliding():
#		perform_jump()

# дальше идет состояние беготни туда сюда если противника (игрока) не видно

var wanderTime : float = 3.0
func wander(delta):
	if wandering and not knockback:
		wanderTime -= delta # он всегда доходит быстрее чем этот таймер истекает поэтому второй не нужен
		if wanderTime < 0:
			nav.target_position = position
			wanderTime = randf_range(3, 4)
			if right_wall.is_colliding(): # если уперся в правую стенку
				nav.target_position.x = position.x + randf_range(-75, -40)
				return
			if left_wall.is_colliding(): # если уперся в левую стенку
				nav.target_position.x = position.x + randf_range(40, 75)
				return
			nav.target_position.x = position.x + randf_range(-75, 75)
	else:
		return

# дальше идет состояние стрельбы по противнику

func attack():
	if timer.is_stopped() and not knockback:
		timer.start()
		aiming = true
		nav.target_position.x = position.x
		velocity.x = 0.0

func shoot(delta):
	if shooting and shot.default_color.a > 0.2:
		aiming = false
		shot.visible = true
		shot.width -= delta / 2
		shot.default_color.a -= delta * 2
		if not shotFired:
			if position.x > player.position.x:
				emit_signal("deal_damage_right")
				for button in attackButtons:
					button.stop_attack()
					shotFired = true
			else:
				emit_signal("deal_damage_left")
				for button in attackButtons:
					button.stop_attack()
				shotFired = true
	else:
		shooting = false
		shot.visible = false
		shotFired = false
		shot.width = 1.0
		shot.default_color.a = 1.0

func _on_timer_timeout():
	shooting = true

func scan_for_player():
	if not shooting:
		if animated_sprite_2d.flip_h == !animated_sprite_2d.flip_h:
			enemy_detect.target_position.x = 0.0
		if abs(enemy_detect.target_position.x) > raycastThreshold: # если рейкаст зашел за свой обозначенный предел, игрока не видно 
			enemySeen = false
			player = null
			enemy_detect.target_position.x = 0.0
		if enemy_detect != null: # во время кадра когда противник умирает, рейкаст становится nullом, и если дать этой функции пройти дальше, она все крашит
			if enemy_detect.is_colliding():
				if enemy_detect.get_collider().is_in_group("Player"):
					enemySeen = true
					if facingLeft:
						enemy_detect.target_position.x += 1.5
					if not facingLeft:
						enemy_detect.target_position.x -= 1.5
					if shot.visible == false:
						shot.points[0].x = enemy_detect.target_position.x
				else:
					enemySeen = false
			else:
				if facingLeft:
					enemy_detect.target_position.x -= 2.5
				elif not facingLeft:
					enemy_detect.target_position.x += 2.5

func take_damage_head(enemyID):
	stunned = false
	aiming = false
	shooting = false
	if !timer.is_stopped():
		timer.stop()
	if selfId == enemyID:
		CURRENT_HEALTH -= 3
		timer.stop()
		if CURRENT_HEALTH <= 0:
			dying = true
		health_changed.emit(CURRENT_HEALTH)
		knockback = true
		knockbackTimer.start()
		if position.x > player.position.x:
			nav.target_position.x = position.x + KNOCKBACK_AMOUNT * 2
		else:
			nav.target_position.x = position.x - KNOCKBACK_AMOUNT * 2

func take_damage_torso(enemyID):
	stunned = false
	aiming = false
	shooting = false
	if !timer.is_stopped():
		timer.stop()
	if selfId == enemyID:
		CURRENT_HEALTH -= 1
		timer.stop()
		if CURRENT_HEALTH <= 0:
			dying = true
		health_changed.emit(CURRENT_HEALTH)
		knockback = true
		knockback_no_stun.start()
		if position.x > player.position.x:
			nav.target_position.x = position.x + KNOCKBACK_AMOUNT
		else:
			nav.target_position.x = position.x - KNOCKBACK_AMOUNT

func take_damage_legs(enemyID):
	stunned = false
	aiming = false
	shooting = false
	if !timer.is_stopped():
		timer.stop()
	if selfId == enemyID:
		CURRENT_HEALTH -= 2
		timer.stop()
		if CURRENT_HEALTH <= 0:
			dying = true
		health_changed.emit(CURRENT_HEALTH)
		knockback = true
		knockbackTimer.start()
		if position.x > player.position.x:
			nav.target_position.x = position.x + KNOCKBACK_AMOUNT
		else:
			nav.target_position.x = position.x - KNOCKBACK_AMOUNT

func _on_knockback_timeout():
	knockback = false
	nav.target_position.x = position.x
	stunned = true
	stunTimer.start()

func _on_stun_timeout():
	stunned = false

func _on_knockback_no_stun_timeout():
	knockback = false
	nav.target_position.x = position.x
