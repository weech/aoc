module Tests

open System
open Xunit

open AOC

//printfn "Solution is %d" (Day01.part1 (Parsers.day01 ()))

[<Fact>]
let ``D1P1`` () =
    let data =
        [ 199
          200
          208
          210
          200
          207
          240
          269
          260
          263 ]
        |> Array.ofList

    Assert.Equal(7, Day01.part1 data)
    Assert.Equal(1121, Day01.part1 (Parsers.day01 ()))

[<Fact>]
let ``D1P2`` () =
    let data =
        [ 199
          200
          208
          210
          200
          207
          240
          269
          260
          263 ]
        |> Array.ofList

    Assert.Equal(5, Day01.part2 data)
    Assert.Equal(1065, Day01.part2 (Parsers.day01 ()))

[<Fact>]
let ``D2P1`` () =
    let data =
        [ "forward 5"
          "down 5"
          "forward 8"
          "up 3"
          "down 8"
          "forward 2" ]

    Assert.Equal(150, Day02.part1 data)
    Assert.Equal(2272262, Day02.part1 (Parsers.day02 ()))

[<Fact>]
let ``D2P2`` () =
    let data =
        [ "forward 5"
          "down 5"
          "forward 8"
          "up 3"
          "down 8"
          "forward 2" ]

    Assert.Equal(900, Day02.part2 data)
    Assert.Equal(2134882034, Day02.part2 (Parsers.day02 ()))

[<Fact>]
let ``D3P1`` () =
    let data =
        [| "00100"
           "11110"
           "10110"
           "10111"
           "10101"
           "01111"
           "00111"
           "11100"
           "10000"
           "11001"
           "00010"
           "01010" |]

    Assert.Equal(198, Day03.part1 data)
    Assert.Equal(749376, Day03.part1 (Parsers.day03 ()))

[<Fact>]
let ``D3P2`` () =
    let data =
        [| "00100"
           "11110"
           "10110"
           "10111"
           "10101"
           "01111"
           "00111"
           "11100"
           "10000"
           "11001"
           "00010"
           "01010" |]

    Assert.Equal(230, Day03.part2 data)
    Assert.Equal(2372923, Day03.part2 (Parsers.day03 ()))

[<Fact>]
let ``D4P1`` () =
    let calls =
        "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1"
            .Split(',')
        |> Seq.map int
        |> Array.ofSeq

    let boards =
        [ "22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19"

          "3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6"

          "14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7" ]

    Assert.Equal(4512, Day04.part1 calls boards)
    let (calls, boards) = Parsers.day04 ()
    Assert.Equal(25410, Day04.part1 calls boards)

[<Fact>]
let ``D4P2`` () =
    let calls =
        "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1"
            .Split(',')
        |> Seq.map int
        |> Array.ofSeq

    let boards =
        [ "22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19"

          "3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6"

          "14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7" ]

    Assert.Equal(1924, Day04.part2 calls boards)
    let (calls, boards) = Parsers.day04 ()
    Assert.Equal(2730, Day04.part2 calls boards)

[<Fact>]
let ``D5P1`` () =
    let data =
        "0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2"
            .Split('\n')

    Assert.Equal(5, Day05.part1 data)
    Assert.Equal(6856, Day05.part1 (Parsers.day05 ()))

[<Fact>]
let ``D5P2`` () =
    let data =
        "0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2"
            .Split('\n')

    Assert.Equal(12, Day05.part2 data)
    Assert.Equal(20666, Day05.part2 (Parsers.day05 ()))

[<Fact>]
let ``D6P1`` () =
    let data =
        "3,4,3,1,2".Split(',')
        |> Seq.map uint64
        |> List.ofSeq

    Assert.Equal(5934UL, Day06.part1 data)
    Assert.Equal(349549UL, Day06.part1 (Parsers.day06 ()))

[<Fact>]
let ``D6P2`` () =
    let data =
        "3,4,3,1,2".Split(',')
        |> Seq.map uint64
        |> List.ofSeq

    Assert.Equal(26984457539UL, Day06.part2 data)
    Assert.Equal(1589590444365UL, Day06.part2 (Parsers.day06 ()))

[<Fact>]
let ``D7P1`` () =
    let data =
        "16,1,2,0,4,2,7,1,2,14".Split(',')
        |> Array.map int

    Assert.Equal(37, Day07.part1 data)
    Assert.Equal(355764, Day07.part1 (Parsers.day07 ()))

[<Fact>]
let ``D7P2`` () =
    let data =
        "16,1,2,0,4,2,7,1,2,14".Split(',')
        |> Array.map int

    Assert.Equal(168, Day07.part2 data)
    Assert.Equal(99634572, Day07.part2 (Parsers.day07 ()))

[<Fact>]
let ``D8P1`` () =
    let data =
        [ "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe"
          "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc"
          "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg"
          "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb"
          "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea"
          "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"
          "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe"
          "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef"
          "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb"
          "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce" ]

    Assert.Equal(26, Day08.part1 data)
    Assert.Equal(355, Day08.part1 (Parsers.day08 ()))

[<Fact>]
let ``D8P2`` () =
    let data =
        [ "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe"
          "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc"
          "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg"
          "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb"
          "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea"
          "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"
          "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe"
          "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef"
          "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb"
          "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce" ]

    Assert.Equal(61229, Day08.part2 data)
    Assert.Equal(983030, Day08.part2 (Parsers.day08 ()))