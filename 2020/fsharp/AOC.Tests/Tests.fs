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

[<Fact>]
let ``D2P1`` () = 
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(2, Scripts.day2Part1 data)
    Assert.Equal(603, Scripts.day2Part1 (Parsers.day2()))

[<Fact>]
let ``D2P2`` () =
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(Scripts.day2Part2 data, 1)
    Assert.Equal(Scripts.day2Part2 (Parsers.day2()), 404)
