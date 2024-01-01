extends CharacterBody2D
class_name Enemy

var SPEED = 60.0
var previous_position = Vector2.ZERO
const ACCELERATION = 400.0
const KNOCKBACK_AMOUNT = 10.0
var FRICTION = 6000.0
const JUMP_VELOCITY = -200.0
var MAXIMUM_HEALTH := 3
@onready var CURRENT_HEALTH := MAXIMUM_HEALTH
var enemySeen = false
var wandering = false
var waiting = false
var facingLeft = false
var destination: float
var shooting = false
var raycastThreshold = 85.0
var shotFired = false
var knockback := false
var dying := false
var aiming := false
var stunned := false
var attackButtons
signal deal_damage_right
signal deal_damage_left
signal health_changed
var selfId = self.get_instance_id()
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

