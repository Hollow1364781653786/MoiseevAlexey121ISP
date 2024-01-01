extends Area2D


# Called when the node enters the scene tree for the first time.
var direction := Vector2.RIGHT
var speed := 3500.0
@onready var collision_shape_2d = $CollisionShape2D

func _physics_process(delta):
	position += direction * speed * delta


func _on_visible_on_screen_notifier_2d_screen_exited():
	queue_free()
