extends State
class_name enemyPatrol

@export var enemyBody: CharacterBody2D
@onready var nav = $"../../NavigationAgent2D"
var enemyDirection: float
var destination: float
var wanderTime : float
var waitTime : float
func wander(delta):
	enemyBody.velocity.x = move_toward(enemyBody.velocity.x, enemyDirection, enemyBody.SPEED)
	if wanderTime == 0:
		waitTime = randf_range(-1, 1)
		random_wait(delta)
	else:
		wanderTime -= delta
func random_wait(delta: float):
	if waitTime == 0:
		enemyDirection = randf_range(-1, 1)
		wanderTime = randf_range(1, 5)
		wander(delta)
	else:
		waitTime -= delta
	print("waiting")

func Enter(delta):
	wander(delta)
	
func Update(delta: float):
	if wanderTime > 0:
		waitTime -= delta
	else:
		random_wait(delta)
