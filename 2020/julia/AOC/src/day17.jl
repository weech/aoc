module Day17 
import AOC.Parsers
using Base.Iterators

function cubify(grid)
	cube = zeros(Bool, size(grid, 1) + 12, size(grid, 2) + 12, 13)
	for row in 1:size(grid, 1), col in 1:size(grid, 2)
		cube[row+6, col+6, 7] = grid[row, col] == '#'
	end
	cube
end

function hyperify(grid)
	cube = zeros(Bool, size(grid, 1) + 12, size(grid, 2) + 12, 13, 13)
	for row in 1:size(grid, 1), col in 1:size(grid, 2)
		cube[row+6, col+6, 7, 7] = grid[row, col] == '#'
	end
	cube
end

get_neighbors(index, arr, ndim) = [index + CartesianIndex(x) for x in product(take(repeated(-1:1), ndim)...)
							 if checkbounds(Bool, arr, index + CartesianIndex(x))]

function cycle!(output, input, ndim)
	for idx in CartesianIndices(input)
		neighbors = get_neighbors(idx, input, ndim)
		if input[idx]
			num = count(input[neighbors])
			output[idx] = num == (2 + 1) || num == (3 + 1) # +1 for self
		else
			output[idx] = count(input[neighbors]) == 3
		end
	end
end

function part1(;data=Parsers.day17())
	cube = cubify(data)
	old = copy(cube)
	for c in 1:6	
		cycle!(old, cube, 3)
		old, cube = cube, old 
	end
	count(cube)
end

function part2(;data=Parsers.day17())
	cube = hyperify(data)
	old = copy(cube)
	for c in 1:6	
		cycle!(old, cube, 4)
		old, cube = cube, old 
	end
	count(cube)
end
end