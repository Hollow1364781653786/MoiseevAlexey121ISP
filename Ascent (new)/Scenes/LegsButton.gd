extends shotButton

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	for button in attackButtons:
		if button == null:
			attackButtons.erase(button)
			break # выходим из цикла, потому что сам он из себя не выходит
		if button != self:
			button.invisible.connect(_on_invisible)
			button.goVisible.connect(_on_go_visible)
	Pos = get_parent().global_position
	uid = self.get_parent().get_instance_id()

func _on_pressed():
	invisible.emit()
	request_aim_legs.emit(Pos)
	if timer.is_stopped():
		timer.start(1.5)

func _on_timer_timeout():
	visible = true
	goVisible.emit()
	request_leg_attack.emit(uid)

func _on_invisible():
	visible = false

func _on_go_visible():
	visible = true

func stop_attack():
	visible = true
	goVisible.emit()
	if !timer.is_stopped():
		timer.stop()
