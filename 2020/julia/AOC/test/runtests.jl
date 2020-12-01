using Test
import AOC.Scripts
@testset "AOC2020" begin 

# Day 1
data = [1721, 979, 366, 299, 675, 1456]
@test 514579 == Scripts.day1_part1(data=data)
@test 842016 == Scripts.day1_part1()
@test 241861950 == Scripts.day1_part2(data=data)
@test 9199664 == Scripts.day1_part2()

end