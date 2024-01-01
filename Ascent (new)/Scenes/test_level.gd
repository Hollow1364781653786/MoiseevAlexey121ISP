extends Node2D

@onready var shot = $Shot
@onready var player_health_bar = $CanvasLayer/PlayerHealthBar
@onready var player_pathfinding = $Player_Pathfinding
@onready var collision_polygon_2d = $StaticBody2D/CollisionPolygon2D
@onready var polygon_2d = $StaticBody2D/CollisionPolygon2D/Polygon2D
var attackButtons
var enemiesSpawned := []
var terrain_y_position = 0
var enemySpawn = preload("res://Scenes/enemy.tscn")
var shooting_head = false
var shooting_torso = false
var shooting_legs = false
var shotFired = false

func _ready():
	player_health_bar.set_max_hearts(player_pathfinding.MAXIMUM_HEALTH)
	player_pathfinding.connect("health_changed", player_health_bar.update_hearts)
	player_health_bar.update_hearts(player_pathfinding.CURRENT_HEALTH)
	player_pathfinding.connect("target_found_head", acquire_target_head)
	player_pathfinding.connect("target_found_torso", acquire_target_torso)
	player_pathfinding.connect("target_found_legs", acquire_target_legs)
	player_pathfinding.connect("deal_damage_head", start_shoot_head)
	player_pathfinding.connect("deal_damage_torso", start_shoot_torso)
	player_pathfinding.connect("deal_damage_legs", start_shoot_legs)
	RenderingServer.set_default_clear_color(Color.hex(000044))
	polygon_2d.polygon = collision_polygon_2d.polygon

func _physics_process(delta):
	shot.points[0] = player_pathfinding.position + Vector2(0, -7)
	shoot_head(delta)
	shoot_torso(delta)
	shoot_legs(delta)
	attackButtons = get_tree().get_nodes_in_group("Buttons")
	for atckButn in attackButtons:
		atckButn.request_head_attack.connect(player_pathfinding.shoot_head)
		atckButn.request_aim_head.connect(player_pathfinding.start_attack_head)
		atckButn.request_aim_torso.connect(player_pathfinding.start_attack_torso)
		atckButn.request_torso_attack.connect(player_pathfinding.shoot_torso)
		atckButn.request_leg_attack.connect(player_pathfinding.shoot_legs)
		atckButn.request_aim_legs.connect(player_pathfinding.start_attack_legs)
		player_pathfinding.stop_attack.connect(atckButn.stop_attack)

func instantiateEnemy(pos):
	var instance = enemySpawn.instantiate()
	instance.position = pos
	add_child(instance)
	enemiesSpawned.append(instance)

func _on_timer_timeout():
	instantiateEnemy(Vector2(randf_range(200, 300), 96))

func acquire_target_head(targetPos):
	shot.points[1] = targetPos + Vector2(0, -15.5)

func acquire_target_torso(targetPos):
	shot.points[1] = targetPos + Vector2(0, -6.5)

func acquire_target_legs(targetPos):
	shot.points[1] = targetPos + Vector2(0, -1.5)

func start_shoot_head(enemyID):
	shooting_head = true

func start_shoot_torso(enemyID):
	shooting_torso = true

func start_shoot_legs(enemyID):
	shooting_legs = true

func shoot_head(delta):
	if shooting_head:
		if shot.default_color.a > 0.2:
			shot.visible = true
			shot.width -= delta / 2
			shot.default_color.a -= delta * 2
		else:
			shooting_head = false
			shot.visible = false
			shot.width = 1
			shot.default_color.a = 1

func shoot_torso(delta):
	if shooting_torso:
		if shot.default_color.a > 0.2:
			shot.visible = true
			shot.width -= delta / 2
			shot.default_color.a -= delta * 2
		else:
			shooting_torso = false
			shot.visible = false
			shot.width = 1
			shot.default_color.a = 1

func shoot_legs(delta):
	if shooting_legs:
		if shot.default_color.a > 0.2:
			shot.visible = true
			shot.width -= delta / 2
			shot.default_color.a -= delta * 2
		else:
			shooting_legs = false
			shot.visible = false
			shot.width = 1
			shot.default_color.a = 1
