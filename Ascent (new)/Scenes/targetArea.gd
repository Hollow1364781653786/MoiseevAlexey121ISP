extends Area2D

func get_collider() -> Node2D:
	var collider = null
	# Replace "Area2D" with the node type you want to retrieve as the collider
	var bodies = get_overlapping_bodies()
	for body in bodies:
		if body:
			collider = body
			break

	return collider
