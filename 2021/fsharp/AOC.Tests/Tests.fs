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
