module Day11 
import AOC.Parsers

const offsets = [CartesianIndex(-1, 0), CartesianIndex(1, 0),
				 CartesianIndex(0, -1), CartesianIndex(0, 1),
				 CartesianIndex(1, 1), CartesianIndex(-1, 1),
				 CartesianIndex(1, -1), CartesianIndex(-1, -1)]

function step_sim!(newboard, counter, tolerance, oldboard)
	for idx in CartesianIndices(oldboard) 
		if oldboard[idx] == 'L' && counter(oldboard, idx) == 0
			 newboard[idx] = '#'
		elseif oldboard[idx] == '#' && counter(oldboard, idx) >= tolerance
			newboard[idx] = 'L'
		else 
			newboard[idx] = oldboard[idx]
		end
	end
end

function run_sim(counter, tolerance, board)
	starting = 1
	ending = count(x -> x == '#', board)
	mutboard = copy(board)
	while starting - ending != 0
		step_sim!(mutboard, counter, tolerance, board)
		board, mutboard = mutboard, board
		starting, ending = ending, count(x -> x == '#', board)
	end
	ending
end

function count_adjacent(board, idx)
	count(offsets) do off 
		newidx = idx + off
		if checkbounds(Bool, board, newidx)
			board[newidx] == '#'
		else 
			false 
		end
	end
end

part1(;data=Parsers.day11()) = run_sim(count_adjacent, 4, data)

function run_length(board, off, idx)
	distance = 1
	while checkbounds(Bool, board, off*distance + idx)
		if board[off*distance + idx] == 'L'
			return false 
		elseif board[off*distance + idx] == '#'
			return true 
		end
		distance += 1
	end
	false
end

count_visible(board, idx) = count(off -> run_length(board, off, idx), offsets)

part2(;data=Parsers.day11()) = run_sim(count_visible, 5, data)

end