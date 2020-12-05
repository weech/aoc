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
@test 7 == Scripts.day3_part1(data=data)
@test 265 == Scripts.day3_part1()
@test 336 == Scripts.day3_part2(data=data)
@test 3154761400 == Scripts.day3_part2()

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
@test 2 == Scripts.day4_part1(data=data)
@test 260 == Scripts.day4_part1()
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
@test 0 == Scripts.day4_part2(data=invalid)
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
@test 4 == Scripts.day4_part2(data=valid)
@test 153 == Scripts.day4_part2()
end