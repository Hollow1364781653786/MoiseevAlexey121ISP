extends CharacterBody2D

@onready var enemy_detect = $EnemyDetect
@onready var knockbackTimer = $Knockback
#@onready var right_wall = $RightWall #рейкаст для проверки стены справа
#@onready var left_wall = $LeftWall #рейкаст для проверки стены слева
@onready var nav: NavigationAgent2D = $NavigationAgent2D #штука, осуществляющая нахождение пути
@onready var animated_sprite_2d = $AnimatedSprite2D 
@onready var CURRENT_HEALTH := MAXIMUM_HEALTH
@export var SPEED = 120.0
@onready var shot = $EnemyDetect2/Shot
@onready var enemy_detect_2 = $EnemyDetect2
@onready var target_area = $targetArea

var enemies = []
var MAXIMUM_HEALTH := 3
var previous_position = Vector2.ZERO
const ACCELERATION = 400.0
var FRICTION = 4000.0
var knockback := false
const IMPULSE_VELOCITY = -170.0
const KNOCKBACK_AMOUNT = 30.0
var isColliding
var target
var enemyID
var shooting_head = false
var shooting_torso = false
var shooting_legs = false
var shotFired = false
var aiming_head := false
var aiming_torso := false
var aiming_legs := false
var dying := false
var targetPos
var attackButton
var slowMotion := true
signal health_changed
signal target_found_head(targetPos)
signal target_found_torso(targetPos)
signal target_found_legs(targetPos)
signal deal_damage_head(id)
signal deal_damage_torso(id)
signal deal_damage_legs(id)
signal stop_attack

# Get the gravity from the project settings to besss synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _ready():
	nav.target_position = position

func _unhandled_input(event):
	if event.is_action_pressed("Left_click") and not knockback:#место назначение выдается по клику
		if not aiming_head or not aiming_torso or not aiming_legs:
			nav.target_position = get_global_mouse_position()

func _physics_process(delta):
	var direction := Vector2()
	direction = nav.get_next_path_position() - global_position
	var direction_axis
	direction_axis = direction.normalized()
	apply_gravity(delta)
	apply_friction(direction_axis, delta)
	apply_pathfinding(delta, direction_axis, direction)
	update_animation(direction_axis)
	move_and_slide()
	manage_signals()
	apply_slow_mo()
	shoot_signal(delta)

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y += gravity * delta

#func perform_jump():
#	if is_on_floor():
#		velocity.y = IMPULSE_VELOCITY

func apply_friction(direction_axis, delta):
	if direction_axis.x == 0:
		velocity.x = move_toward(velocity.x, 0, FRICTION * delta)
	if abs(velocity.x) > SPEED - 20.0 and not knockback:
		FRICTION *= 5

func update_animation(direction_axis):
	var current_position = position
	if not dying:
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
		if not is_on_floor() or knockback:
			animated_sprite_2d.play("jumping")
		previous_position = current_position #эти две переменные нужны только для того чтобы проверять изменения позиции
	else: # if dying
		animated_sprite_2d.play("death")
		var currentFrame = animated_sprite_2d.frame
		var totalFrames = animated_sprite_2d.sprite_frames.get_frame_count("death")
		if currentFrame == totalFrames - 1:
			queue_free()
	if shooting_head or shooting_torso or shooting_legs:
		animated_sprite_2d.play("shoot")
	if aiming_head or aiming_torso or aiming_legs: # если скоро выстрелит
		animated_sprite_2d.play("aim")

@warning_ignore("unused_parameter")
func apply_pathfinding(delta, direction_axis, direction):
	nav.target_position.y = position.y - 15.0 #точка назначения фиксируется над персом чтобы он замедлялся
	nav.path_desired_distance = 20.0
	nav.target_desired_distance = 20.0
#	if raycastRight.is_colliding() and nav.distance_to_target() > 20.0 and velocity.x > 0 and !right_wall.is_colliding() and not knockback:
#		perform_jump()
#	if raycastLeft.is_colliding() and nav.distance_to_target() > 20.0 and velocity.x < 0 and !left_wall.is_colliding() and not knockback:
#		perform_jump()
	if not knockback and not shooting_head or shooting_torso or shooting_legs:
		velocity.x = move_toward(velocity.x, SPEED * direction_axis.x, ACCELERATION * delta) #движение к точке назначения
	else:
		velocity.x = move_toward(velocity.x, SPEED * direction_axis.x, ACCELERATION)
	if nav.target_position.x - position.x == 10.0:
		nav.target_position.x = position.x

func get_shot_right():
	take_damage()
	emit_signal("stop_attack")
	knockback = true
	if knockback:
		if knockbackTimer.is_stopped():
			knockbackTimer.start()
			nav.target_position.x = position.x - KNOCKBACK_AMOUNT

func get_shot_left():
	take_damage()
	emit_signal("stop_attack")
	knockback = true
	if knockback:
		if knockbackTimer.is_stopped():
			knockbackTimer.start()
			nav.target_position.x = position.x + KNOCKBACK_AMOUNT

func _on_detection_area_body_entered(body): # добавление противников в массив всех противников, находящихся рядом с игроком
	if body is Enemy:
		enemies.append(body)

func manage_signals():
	for enemy in enemies:
		enemy.connect("deal_damage_right", get_shot_right)
		enemy.connect("deal_damage_left", get_shot_left)

func _on_knockback_timeout():
	knockback = false
	nav.target_position.x = position.x

func take_damage():
	CURRENT_HEALTH -= 1
	aiming_head = false
	aiming_torso = false
	aiming_legs = false
	shooting_head = false
	shooting_torso = false
	shooting_legs = false
	if CURRENT_HEALTH < 0:
		CURRENT_HEALTH = MAXIMUM_HEALTH
	health_changed.emit(CURRENT_HEALTH)

func start_attack_head(targetPos):
	aiming_head = true
	nav.target_position.x = position.x
	emit_signal("target_found_head", targetPos)
	velocity.x = 0.0
	if targetPos.x < position.x and aiming_head: # игрок поворачивается в сторону противника
		animated_sprite_2d.flip_h = true
	else:
		animated_sprite_2d.flip_h = false

func start_attack_torso(targetPos):
	aiming_torso = true
	nav.target_position.x = position.x
	emit_signal("target_found_torso", targetPos)
	velocity.x = 0.0
	if targetPos.x < position.x and aiming_torso: # игрок поворачивается в сторону противника
		animated_sprite_2d.flip_h = true
	else:
		animated_sprite_2d.flip_h = false

func start_attack_legs(targetPos):
	aiming_legs = true
	nav.target_position.x = position.x
	emit_signal("target_found_legs", targetPos)
	velocity.x = 0.0
	if targetPos.x < position.x and aiming_legs: # игрок поворачивается в сторону противника
		animated_sprite_2d.flip_h = true
	else:
		animated_sprite_2d.flip_h = false

func shoot_signal(delta):
	if shooting_head or shooting_torso or shooting_legs and not shotFired:
		aiming_head = false
		aiming_torso = false
		aiming_legs = false
		shotFired = true
	else:
		shotFired = false
		shooting_head = false
		shooting_torso = false
		shooting_legs = false

func shoot_head(targetID):
	target = targetID
	aiming_head = false
	for enemy in enemies:
		if target:
			if target == enemy.get_instance_id(): # получаем номер узла того противника, в которого совершается выстрел
				enemyID = enemy.get_instance_id()
				targetPos = enemy.position
				emit_signal("target_found_head", targetPos)
				deal_damage_head.emit(target)
			else:
				enemyID = null
		else:
			enemyID = null

func shoot_torso(targetID):
	target = targetID
	aiming_torso = false
	for enemy in enemies:
		if target:
			if target == enemy.get_instance_id(): # получаем номер узла того противника, в которого совершается выстрел
				enemyID = enemy.get_instance_id()
				targetPos = enemy.position
				emit_signal("target_found_torso", targetPos)
				deal_damage_torso.emit(target)
			else:
				enemyID = null
		else:
			enemyID = null

func shoot_legs(targetID):
	target = targetID
	aiming_legs = false
	for enemy in enemies:
		if target:
			if target == enemy.get_instance_id(): # получаем номер узла того противника, в которого совершается выстрел
				enemyID = enemy.get_instance_id()
				targetPos = enemy.position
				emit_signal("target_found_legs", targetPos)
				deal_damage_legs.emit(target)
			else:
				enemyID = null
		else:
			enemyID = null

func _on_detection_area_body_exited(body): #ни в коем случае не удалять, без нее все будет крашиться
	enemies.erase(body)

func apply_slow_mo():
	if not shooting_head or not shooting_torso or not shooting_legs or not aiming_head or not aiming_torso or not aiming_legs:
		slowMotion = abs(velocity.x) < 1
	if aiming_head or aiming_torso or aiming_legs or shooting_head or shooting_torso or shooting_legs:
		slowMotion = false
	if slowMotion:
		Engine.time_scale = 0.1
	else:
		Engine.time_scale = 1
