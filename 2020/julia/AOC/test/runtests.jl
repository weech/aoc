using Test
import AOC.Scripts
@testset "AOC2020" begin 

# Day 1
data = [1721, 979, 366, 299, 675, 1456]
@test 514579 == Scripts.day1_part1(data=data)
@test 842016 == Scripts.day1_part1()
@test 241861950 == Scripts.day1_part2(data=data)
@test 9199664 == Scripts.day1_part2()

# Day 2
data = ["1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"]
@test 2 == Scripts.day2_part1(data=data)
@test 603 == Scripts.day2_part1()
@test 1 == Scripts.day2_part2(data=data)
@test 404 == Scripts.day2_part2()

end