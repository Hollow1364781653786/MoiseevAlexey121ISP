extends Node
signal dynamic_signal(data)

func process_signal(sender, data):
	emit_signal("dynamic_signal", sender, data)
