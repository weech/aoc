module Day12 
import AOC.Parsers

mutable struct Ship 
	direction::Int 
	dx::Int 
	dy::Int
end

Ship() = Ship(90, 0, 0)

function norm_direction(degree)
	modded = degree % 360 
	if modded >= 0 
		modded 
	else
		modded + 360
	end
end

function move_ship!(ship::Ship, instruction)
	if instruction[1] == 'N'
		ship.dy += parse(Int, instruction[2:end])
	elseif instruction[1] == 'S'
		ship.dy -= parse(Int, instruction[2:end])
	elseif instruction[1] == 'E'
		ship.dx += parse(Int, instruction[2:end])
	elseif instruction[1] == 'W'
		ship.dx -= parse(Int, instruction[2:end])
	elseif instruction[1]  == 'L'
		ship.direction -= parse(Int, instruction[2:end])
	elseif instruction[1] == 'R'
		ship.direction += parse(Int, instruction[2:end])
	elseif instruction[1] == 'F'
		if norm_direction(ship.direction) == 0
			ship.dy += parse(Int, instruction[2:end])
		elseif norm_direction(ship.direction) == 90
			ship.dx += parse(Int, instruction[2:end])
		elseif norm_direction(ship.direction) == 180 
			ship.dy -= parse(Int, instruction[2:end])
		elseif norm_direction(ship.direction) == 270 
			ship.dx -= parse(Int, instruction[2:end])
		else
			throw(DomainError(ship.direction, "Invalid direction"))
		end 
	else
		throw(DomainError(instruction, "Invalid instruction"))
	end
end

function part1(;data=Parsers.day12())
	ship = Ship()
	for instruction in data 
		move_ship!(ship, instruction)
	end
	abs(ship.dx) + abs(ship.dy)
end

# I can't think of anything reusable between these problems
# The logic is entirely different and conflicting

mutable struct Waypoint 
	x::Int 
	y::Int 
end

mutable struct Ship2 
	dx::Int 
	dy::Int
	waypoint::Waypoint
end

Ship2() = Ship2(0, 0, Waypoint(10, 1))

function rotate_point!(point, degree)
	point.x, point.y = [cosd(degree) sind(degree); -sind(degree) cosd(degree)] * [point.x; point.y]
end

function move_ship!(ship::Ship2, instruction)
	if instruction[1] == 'N'
		ship.waypoint.y += parse(Int, instruction[2:end])
	elseif instruction[1] == 'S'
		ship.waypoint.y -= parse(Int, instruction[2:end])
	elseif instruction[1] == 'E'
		ship.waypoint.x += parse(Int, instruction[2:end])
	elseif instruction[1] == 'W'
		ship.waypoint.x -= parse(Int, instruction[2:end])
	elseif instruction[1]  == 'L'
		rotate_point!(ship.waypoint, -1*parse(Int, instruction[2:end]))
	elseif instruction[1] == 'R'
		rotate_point!(ship.waypoint, parse(Int, instruction[2:end]))
	elseif instruction[1] == 'F'
		multiple = parse(Int, instruction[2:end])
		ship.dy += ship.waypoint.y * multiple
		ship.dx += ship.waypoint.x * multiple
	else
		throw(DomainError(instruction, "Invalid instruction"))
	end
end

function part2(;data=Parsers.day12())
	ship = Ship2()
	for instruction in data 
		move_ship!(ship, instruction)
	end
	abs(ship.dx) + abs(ship.dy)
end

end