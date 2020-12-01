module Tests

open System
open Xunit

open AOC

[<Fact>]
let ``D1P1`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(Scripts.day1Part1 data, 514579)
    Assert.Equal(Scripts.day1Part1 (Parsers.day1()), 842016)

[<Fact>]
let ``D1P2`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(Scripts.day1Part2 data, 241861950)
    Assert.Equal(Scripts.day1Part2 (Parsers.day1()), 9199664)