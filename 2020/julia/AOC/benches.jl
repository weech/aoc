using BenchmarkTools
using Statistics
import AOC
import AOC.Parsers

suite = BenchmarkGroup()
#=
data1 = Parsers.day1()
suite["D01P1"] = @benchmarkable AOC.day1_part1(data=$data1)
suite["D01P2"] = @benchmarkable AOC.day1_part2(data=$data1)

data2 = Parsers.day2()
suite["D02P1"] = @benchmarkable AOC.day2_part1(data=$data2)
suite["D02P2"] = @benchmarkable AOC.day2_part2(data=$data2)

data3 = Parsers.day3()
suite["D03P1"] = @benchmarkable AOC.day3_part1(data=$data3)
suite["D03P2"] = @benchmarkable AOC.day3_part2(data=$data3)

data4 = Parsers.day4()
suite["D04P1"] = @benchmarkable AOC.day4_part1(data=$data4)
suite["D04P2"] = @benchmarkable AOC.day4_part2(data=$data4)

data5 = collect(Parsers.day5())
suite["D05P1"] = @benchmarkable AOC.day5_part1(data=$data5)
suite["D05P2"] = @benchmarkable AOC.day5_part2(data=$data5)

data6 = Parsers.day6()
suite["D06P1"] = @benchmarkable AOC.day6_part1(data=$data6)
suite["D06P2"] = @benchmarkable AOC.day6_part2(data=$data6)

data7 = collect(Parsers.day7())
suite["D07P1"] = @benchmarkable AOC.Day7.part1(data=$data7)
suite["D07P2"] = @benchmarkable AOC.Day7.part2(data=$data7)

data11a = Parsers.day11()
data11b = copy(data11a)
suite["D11P1"] = @benchmarkable AOC.Day11.part1(data=$data11a)
suite["D11P2"] = @benchmarkable AOC.Day11.part2(data=$data11b)
=#
data14 = collect(Parsers.day14())
suite["D14P1"] = @benchmarkable AOC.Day14.part1(data=$data14)
suite["D14P2"] = @benchmarkable AOC.Day14.part2(data=$data14)

tune!(suite)
results = run(suite, verbose = true)
if length(ARGS) > 0 && ARGS[1] == "display"
	for (name, trial) in results 
		println(name)
		display(trial)
		println()
	end
end

#=
medians = median(results)
list = map(medians) do (name, est)
	(name, est.time)
end
sort!(list)

const DATA = joinpath(@__DIR__, "..", "..", "data")
open(joinpath(DATA, "benches_jl.csv"), "w") do f 
	for (name, est) in list
		println(f, name, ",", est / 1000.)
	end
end
=#