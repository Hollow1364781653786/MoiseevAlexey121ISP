extends CharacterBody2D

@onready var animated_sprite_2d = $AnimatedSprite2D
@export var SPEED = 50.0
const ACCELERATION = 400.0
const FRICTION = 2000.0
const JUMP_VELOCITY = -200.0
@onready var nav: NavigationAgent2D = $NavigationAgent2D

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")


func _physics_process(delta):
	apply_gravity(delta)
	handle_jump()
	var input_axis = Input.get_axis("ui_left", "ui_right")
	apply_friction(input_axis, delta)
	apply_acceleration(input_axis, delta)
	update_animation(input_axis)
	nav.target_position = get_global_mouse_position()
	move_and_slide()

func apply_gravity(delta):
	if not is_on_floor():
		velocity.y += gravity * delta

func handle_jump():
	if is_on_floor():
		if Input.is_action_just_pressed("ui_accept"):
			velocity.y = JUMP_VELOCITY
	else:
		if Input.is_action_just_released("ui_accept") and velocity.y < 0:
			velocity.y = JUMP_VELOCITY / 2
	
func apply_friction(input_axis, delta):
	if input_axis == 0:
		velocity.x = move_toward(velocity.x, 0, FRICTION * delta)
	
func apply_acceleration(input_axis, delta):
	if input_axis:
		velocity.x = move_toward(velocity.x, SPEED * input_axis, ACCELERATION * delta)
func update_animation(input_axis):
	if velocity == Vector2():
		animated_sprite_2d.play("idle")
	if input_axis != 0:
		animated_sprite_2d.flip_h = input_axis < 0
		animated_sprite_2d.play("running")
	if not is_on_floor():
		animated_sprite_2d.play("jumping")

