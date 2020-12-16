module Day15 

function whats_next!(recitation, previous, turn)
	if haskey(recitation, previous)
		last_said = recitation[previous]
		recitation[previous] = turn-1
		turn - last_said - 1
	else
		recitation[previous] = turn-1
		0 
	end
end

function run_game(data, ending)
	recitation = Dict(zip(data[1:end-1], 1:(length(data)-1)))
	previous = data[end]
	for turn in (length(data)+1):ending
		previous = whats_next!(recitation, previous, turn)
	end
	previous
end

part1(;data=[1,0,15,2,10,13]) = run_game(data, 2020)

part2(;data=[1,0,15,2,10,13]) = run_game(data, 30000000)

end