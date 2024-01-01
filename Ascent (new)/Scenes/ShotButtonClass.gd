extends TextureButton
class_name shotButton

@onready var timer = $Timer

signal request_aim_head(Pos)
signal request_aim_torso(Pos)
signal request_aim_legs(Pos)
signal request_head_attack(uid)
signal request_torso_attack(uid)
signal request_leg_attack(uid)
signal invisible
signal goVisible
var uid
var Pos
var attackButtons

func _ready():
	attackButtons = get_tree().get_nodes_in_group("Buttons")
	uid = self.get_parent().get_instance_id()
