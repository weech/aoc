module Tests

open System
open Xunit

open AOC

[<Fact>]
let ``D1P1`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(514579, Day1.part1 data)
    Assert.Equal(842016, Day1.part1 (Parsers.day1()))

[<Fact>]
let ``D1P2`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(241861950, Day1.part2 data)
    Assert.Equal(9199664, Day1.part2 (Parsers.day1()))

[<Fact>]
let ``D2P1`` () = 
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(2, Day2.part1 data)
    Assert.Equal(603, Day2.part1 (Parsers.day2()))

[<Fact>]
let ``D2P2`` () =
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(1, Day2.part2 data)
    Assert.Equal(404, Day2.part2 (Parsers.day2()))


[<Fact>]
let ``D3P1`` () = 
    let data = array2D [ [0; 0; 1; 1; 0; 0; 0; 0; 0; 0; 0 ];
        [1; 0; 0; 0; 1; 0; 0; 0; 1; 0; 0 ];
        [0; 1; 0; 0; 0; 0; 1; 0; 0; 1; 0 ];
        [0; 0; 1; 0; 1; 0; 0; 0; 1; 0; 1 ];
        [0; 1; 0; 0; 0; 1; 1; 0; 0; 1; 0 ];
        [0; 0; 1; 0; 1; 1; 0; 0; 0; 0; 0 ];
        [0; 1; 0; 1; 0; 1; 0; 0; 0; 0; 1 ];
        [0; 1; 0; 0; 0; 0; 0; 0; 0; 0; 1 ];
        [1; 0; 1; 1; 0; 0; 0; 1; 0; 0; 0 ];
        [1; 0; 0; 0; 1; 1; 0; 0; 0; 0; 1 ];
        [0; 1; 0; 0; 1; 0; 0; 0; 1; 0; 1 ]]
    Assert.Equal(7L, Day3.part1 data)
    Assert.Equal(265L, Day3.part1 (Parsers.day3()))

[<Fact>]
let ``D3P2`` () = 
    let data = array2D [ [0; 0; 1; 1; 0; 0; 0; 0; 0; 0; 0 ];
        [1; 0; 0; 0; 1; 0; 0; 0; 1; 0; 0 ];
        [0; 1; 0; 0; 0; 0; 1; 0; 0; 1; 0 ];
        [0; 0; 1; 0; 1; 0; 0; 0; 1; 0; 1 ];
        [0; 1; 0; 0; 0; 1; 1; 0; 0; 1; 0 ];
        [0; 0; 1; 0; 1; 1; 0; 0; 0; 0; 0 ];
        [0; 1; 0; 1; 0; 1; 0; 0; 0; 0; 1 ];
        [0; 1; 0; 0; 0; 0; 0; 0; 0; 0; 1 ];
        [1; 0; 1; 1; 0; 0; 0; 1; 0; 0; 0 ];
        [1; 0; 0; 0; 1; 1; 0; 0; 0; 0; 1 ];
        [0; 1; 0; 0; 1; 0; 0; 0; 1; 0; 1 ]]
    Assert.Equal(336L, Day3.part2 data)
    Assert.Equal(3154761400L, Day3.part2 (Parsers.day3()))

[<Fact>]
let ``D4P1`` () = 
    let data = ["ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
    byr:1937 iyr:2017 cid:147 hgt:183cm";
    "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
    hcl:#cfa07d byr:1929";
    "hcl:#ae17e1 iyr:2013
    eyr:2024
    ecl:brn pid:760753108 byr:1931
    hgt:179cm";
    "hcl:#cfa07d eyr:2025 pid:166559648
    iyr:2011 ecl:brn hgt:59in";
    ]
    Assert.Equal(2, Day4.part1 data)
    Assert.Equal(260, Day4.part1 (Parsers.day4()))

[<Fact>]
let ``D4P2`` () = 
    let invalid = ["eyr:1972 cid:100
    hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926";
    "iyr:2019
    hcl:#602927 eyr:1967 hgt:170cm
    ecl:grn pid:012533040 byr:1946";
    "hcl:dab227 iyr:2012
    ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277";
    "hgt:59cm ecl:zzz
    eyr:2038 hcl:74454a iyr:2023
    pid:3556412378 byr:2007"]
    Assert.Equal(0, Day4.part2 invalid)
    let valid = ["pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
    hcl:#623a2f";
    "eyr:2029 ecl:blu cid:129 byr:1989
    iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm";
    "hcl:#888785
    hgt:164cm byr:2001 iyr:2015 cid:88
    pid:545766238 ecl:hzl
    eyr:2022";
    "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
    ]
    Assert.Equal(4, Day4.part2 valid)
    Assert.Equal(153, Day4.part2 (Parsers.day4()))


[<Fact>]
let ``D5P1`` () = 
    let data = ["FBFBBFFRLR"; "BFFFBBFRRR"; "FFFBBBFRRR"; "BBFFBBFRLL"]
    let truths = [357; 567; 119; 820]
    Seq.zip data truths 
    |> Seq.iter (fun (item, truth) -> 
        Assert.Equal(truth, Day5.getSeatID item)
    )
    Assert.Equal(820, Day5.part1 data)
    Assert.Equal(880, Day5.part1(Parsers.day5()))

[<Fact>]
let ``D5P2`` () =
    Assert.Equal(731, Day5.part2(Parsers.day5()))

[<Fact>]
let ``D6P1`` () = 
    let data = [["abc"]; ["a"; "b"; "c"]; ["ab"; "ac"];
                ["a"; "a"; "a"; "a"]; ["b"]]
    Assert.Equal(11, Day6.part1(data))
    Assert.Equal(6161, Day6.part1(Parsers.day6()))

[<Fact>]
let ``D6P2`` () =
    let data = [["abc"]; ["a"; "b"; "c"]; ["ab"; "ac"];
                ["a"; "a"; "a"; "a"]; ["b"]]
    Assert.Equal(6, Day6.part2(data))
    Assert.Equal(2971, Day6.part2(Parsers.day6()))

[<Fact>]
let ``D7P1`` () = 
    let data = ["light red bags contain 1 bright white bag, 2 muted yellow bags.";
"dark orange bags contain 3 bright white bags, 4 muted yellow bags.";
"bright white bags contain 1 shiny gold bag.";
"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.";
"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.";
"dark olive bags contain 3 faded blue bags, 4 dotted black bags.";
"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.";
"faded blue bags contain no other bags.";
"dotted black bags contain no other bags."]
    Assert.Equal(4, Day7.part1(data))
    Assert.Equal(148, Day7.part1(Parsers.day7()))

[<Fact>]
let ``D7P2`` () =
    let data = ["light red bags contain 1 bright white bag, 2 muted yellow bags.";
    "dark orange bags contain 3 bright white bags, 4 muted yellow bags.";
    "bright white bags contain 1 shiny gold bag.";
    "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.";
    "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.";
    "dark olive bags contain 3 faded blue bags, 4 dotted black bags.";
    "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.";
    "faded blue bags contain no other bags.";
    "dotted black bags contain no other bags."]
    Assert.Equal(32, Day7.part2(data))
    let data2 = ["shiny gold bags contain 2 dark red bags.";
    "dark red bags contain 2 dark orange bags.";
    "dark orange bags contain 2 dark yellow bags.";
    "dark yellow bags contain 2 dark green bags.";
    "dark green bags contain 2 dark blue bags.";
    "dark blue bags contain 2 dark violet bags.";
    "dark violet bags contain no other bags."]
    Assert.Equal(126, Day7.part2(data2))
    Assert.Equal(24867, Day7.part2(Parsers.day7()))

[<Fact>]
let ``D8P1`` () = 
    let data = ["nop +0"; "acc +1"; "jmp +4"; "acc +3"; "jmp -3";
                "acc -99"; "acc +1"; "jmp -4"; "acc +6"]
    Assert.Equal(5, Day8.part1(data))
    Assert.Equal(1818, Day8.part1(Parsers.day8()))

[<Fact>]
let ``D8P2`` () =
    let data = ["nop +0"; "acc +1"; "jmp +4"; "acc +3"; "jmp -3";
                "acc -99"; "acc +1"; "jmp -4"; "acc +6"]
    Assert.Equal(8, Day8.part2(data))
    Assert.Equal(631, Day8.part2(Parsers.day8()))


[<Fact>]
let ``D9P1`` () = 
    let data = [|35L; 20L; 15L; 25L; 47L; 40L; 62L; 55L; 65L; 95L; 
                102L; 117L; 150L; 182L; 127L; 219L; 299L; 277L; 
                309L; 576L|]
    Assert.Equal(127L, Day9.part1 data 5)
    Assert.Equal(1038347917L, Day9.part1 (Parsers.day9()) 25)

[<Fact>]
let ``D9P2`` () =
    let data = [|35L; 20L; 15L; 25L; 47L; 40L; 62L; 55L; 65L; 95L; 
                102L; 117L; 150L; 182L; 127L; 219L; 299L; 277L; 
                309L; 576L|]
    Assert.Equal(62L, Day9.part2 data 5)
    Assert.Equal(137394018L, Day9.part2 (Parsers.day9()) 25)