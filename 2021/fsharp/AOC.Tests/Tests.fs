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