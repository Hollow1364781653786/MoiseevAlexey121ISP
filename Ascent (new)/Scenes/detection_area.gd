extends Area2D

@onready var enemy_scan_left = $enemyScanLeft
@onready var enemy_scan_right = $enemyScanRight
@onready var collision_shape_2d = $CollisionShape2D

var defaultPos = Vector2(0, 100)
var raycasts = []

#func _ready():
#	defaultPosLeft = enemy_scan_left.position.x
#	defaultPosLeft = enemy_scan_right.position.x

func _physics_process(delta):
	enemy_scan_left.position.x -= 5
	enemy_scan_right.position.x += 5
	for body in raycasts:
		print(body)

func _on_body_exited(body):
	if body is RayCast2D:
		print("FUUCK")


func _on_body_entered(body):
	if body is RayCast2D:
		raycasts.append(body)
