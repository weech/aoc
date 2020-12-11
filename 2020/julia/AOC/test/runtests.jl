using Test
import AOC
@testset "AOC2020" begin 

# Day 1
data = [1721, 979, 366, 299, 675, 1456]
@test 514579 == AOC.day1_part1(data=data)
@test 842016 == AOC.day1_part1()
@test 241861950 == AOC.day1_part2(data=data)
@test 9199664 == AOC.day1_part2()

# Day 2
data = ["1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"]
@test 2 == AOC.day2_part1(data=data)
@test 603 == AOC.day2_part1()
@test 1 == AOC.day2_part2(data=data)
@test 404 == AOC.day2_part2()

# Day 3 
data = [0 0 1 1 0 0 0 0 0 0 0
1 0 0 0 1 0 0 0 1 0 0
0 1 0 0 0 0 1 0 0 1 0
0 0 1 0 1 0 0 0 1 0 1
0 1 0 0 0 1 1 0 0 1 0
0 0 1 0 1 1 0 0 0 0 0
0 1 0 1 0 1 0 0 0 0 1
0 1 0 0 0 0 0 0 0 0 1
1 0 1 1 0 0 0 1 0 0 0
1 0 0 0 1 1 0 0 0 0 1
0 1 0 0 1 0 0 0 1 0 1]
@test 7 == AOC.day3_part1(data=data)
@test 265 == AOC.day3_part1()
@test 336 == AOC.day3_part2(data=data)
@test 3154761400 == AOC.day3_part2()

# Day 4
data = ["ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm",
"iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929",
"hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm",
"hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in",
]
@test 2 == AOC.day4_part1(data=data)
@test 260 == AOC.day4_part1()
invalid = ["eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
"iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946",
"hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
"hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007"]
@test 0 == AOC.day4_part2(data=invalid)
valid = [
"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f",
"eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
"hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022",
"iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
]
@test 4 == AOC.day4_part2(data=valid)
@test 153 == AOC.day4_part2()

# Day 5
data = ["FBFBBFFRLR", "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL"]
truths = [357, 567, 119, 820]
for (truth, item) in zip(truths, data)
	@test truth == AOC.Day5.get_seat_id(item)
end
@test 820 == AOC.day5_part1(data=data)
@test 880 == AOC.day5_part1()
@test 731 == AOC.day5_part2()

# Day 6
data = [["abc"], ["a", "b", "c"], ["ab", "ac"], 
		["a", "a", "a", "a"], ["b"]]
@test 11 == AOC.day6_part1(data=data)
@test 6161 == AOC.day6_part1()
@test 6 == AOC.day6_part2(data=data)
@test 2971 == AOC.day6_part2()

# Day 7 
data = ["light red bags contain 1 bright white bag, 2 muted yellow bags.",
"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
"bright white bags contain 1 shiny gold bag.",
"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
"faded blue bags contain no other bags.",
"dotted black bags contain no other bags."]
@test 4 == AOC.Day7.part1(data=data)
@test 148 == AOC.Day7.part1()
data2 = ["shiny gold bags contain 2 dark red bags.",
"dark red bags contain 2 dark orange bags.",
"dark orange bags contain 2 dark yellow bags.",
"dark yellow bags contain 2 dark green bags.",
"dark green bags contain 2 dark blue bags.",
"dark blue bags contain 2 dark violet bags.",
"dark violet bags contain no other bags."]
@test 32 == AOC.Day7.part2(data=data)
@test 126 == AOC.Day7.part2(data=data2)
@test 24867 == AOC.Day7.part2()

# Day 8
data = ["nop +0", "acc +1", "jmp +4", "acc +3", "jmp -3",
		"acc -99", "acc +1", "jmp -4", "acc +6"]
@test 5 == AOC.Day8.part1(data=data)
@test 1818 == AOC.Day8.part1()
@test 8 == AOC.Day8.part2(data=data)

# Day 9 
data = [35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150,
		182, 127, 219, 299, 277, 309, 576]
@test 127 == AOC.Day9.part1(data=data, preamble_len=5)
@test 1038347917 == AOC.Day9.part1()
@test 62 == AOC.Day9.part2(data=data, preamble_len=5)
@test 137394018 == AOC.Day9.part2()

# Day 10 
data1 = [16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4]
data2 = [28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19,
		 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3]
@test 7*5 == AOC.Day10.part1(data=data1)
@test 22*10 == AOC.Day10.part1(data=data2)
@test 1836 == AOC.Day10.part1()
@test 8 == AOC.Day10.part2(data=data1)
@test 19208 == AOC.Day10.part2(data=data2)
@test 43406276662336 == AOC.Day10.part2()
end