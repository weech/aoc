module Tests

open System
open Xunit

open AOC

[<Fact>]
let ``D1P1`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(514579, Scripts.day1Part1 data)
    Assert.Equal(842016, Scripts.day1Part1 (Parsers.day1()))

[<Fact>]
let ``D1P2`` () =
    let data = [1721; 979; 366; 299; 675; 1456]
    Assert.Equal(241861950, Scripts.day1Part2 data)
    Assert.Equal(9199664, Scripts.day1Part2 (Parsers.day1()))

[<Fact>]
let ``D2P1`` () = 
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(2, Scripts.day2Part1 data)
    Assert.Equal(603, Scripts.day2Part1 (Parsers.day2()))

[<Fact>]
let ``D2P2`` () =
    let data = ["1-3 a: abcde"; "1-3 b: cdefg"; "2-9 c: ccccccccc"]
    Assert.Equal(1, Scripts.day2Part2 data)
    Assert.Equal(404, Scripts.day2Part2 (Parsers.day2()))


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
    Assert.Equal(7L, Scripts.day3Part1 data)
    Assert.Equal(265L, Scripts.day3Part1 (Parsers.day3()))

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
    Assert.Equal(336L, Scripts.day3Part2 data)
    Assert.Equal(3154761400L, Scripts.day3Part2 (Parsers.day3()))