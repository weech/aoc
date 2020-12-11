module Day10
import AOC.Parsers

function part1(;data=Parsers.day10())
	differences = diff(sort(vcat([0], data)))
	count(differences .== 1) * (count(differences .== 3) + 1)
end

function part2(;data=Parsers.day10())
	differences = diff(sort(vcat([0], data)))
	grouplens = [0]
	for d in differences 
		if d == 1
			grouplens[end] += 1
		else
			push!(grouplens, 0)
		end
	end
	num2s = count(grouplens .== 2)
	num3s = count(grouplens .== 3)
	num4s = count(grouplens .== 4)
	2^num2s * 4^num3s * 7^num4s
end

end