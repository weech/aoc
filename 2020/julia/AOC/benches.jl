using BenchmarkTools
using Statistics
import AOC
import AOC.Parsers

data1 = Parsers.day1()
data2 = Parsers.day2()
data3 = Parsers.day3()
data4 = Parsers.day4()
data5 = collect(Parsers.day5())
data6 = Parsers.day6()
data7 = collect(Parsers.day7())

suite = BenchmarkGroup()

suite["D01P1"] = @benchmarkable AOC.day1_part1(data=$data1)
suite["D01P2"] = @benchmarkable AOC.day1_part2(data=$data1)

suite["D02P1"] = @benchmarkable AOC.day2_part1(data=$data2)
suite["D02P2"] = @benchmarkable AOC.day2_part2(data=$data2)

suite["D03P1"] = @benchmarkable AOC.day3_part1(data=$data3)
suite["D03P2"] = @benchmarkable AOC.day3_part2(data=$data3)

suite["D04P1"] = @benchmarkable AOC.day4_part1(data=$data4)
suite["D04P2"] = @benchmarkable AOC.day4_part2(data=$data4)

suite["D05P1"] = @benchmarkable AOC.day5_part1(data=$data5)
suite["D05P2"] = @benchmarkable AOC.day5_part2(data=$data5)

suite["D06P1"] = @benchmarkable AOC.day6_part1(data=$data6)
suite["D06P2"] = @benchmarkable AOC.day6_part2(data=$data6)

suite["D07P1"] = @benchmarkable AOC.Day7.part1(data=$data7)
suite["D07P2"] = @benchmarkable AOC.Day7.part2(data=$data7)



tune!(suite)
results = run(suite, verbose = true)
if length(ARGS) > 0 && ARGS[1] == "display"
	for (name, trial) in results 
		println(name)
		display(trial)
		println()
	end
end

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
