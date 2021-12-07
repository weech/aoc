open System
open AOC
open BenchmarkDotNet.Attributes 
open BenchmarkDotNet.Running 

type AOCPerformance() = 

    let data1 = Parsers.day01()
    let data2a = Parsers.day02()
    let data2b = Parsers.day02()
    let data3 = Parsers.day03()
    let (turns, boards) = Parsers.day04()
    let data5 = Parsers.day05()
    let data7 = Parsers.day07()

    (*
    [<Benchmark>]
    member this.D1P1() = Day01.part1 data1
    [<Benchmark>]
    member this.D1P2() = Day01.part2 data1

    [<Benchmark>]
    member this.D2P1() = Day02.part1 data2a
    [<Benchmark>]
    member this.D2P2() = Day02.part2 data2b

    [<Benchmark>]
    member this.D3P1() = Day03.part1 data3
    [<Benchmark>]
    member this.D3P2() = Day03.part2 data3

    [<Benchmark>]
    member this.D4P1() = Day04.part1 turns boards
    [<Benchmark>]
    member this.D4P2() = Day04.part2 turns boards
 
    [<Benchmark>]
    member this.D5P1() = Day05.part1 data5
    [<Benchmark>]
    member this.D5P2() = Day05.part2 data5
       *)
    [<Benchmark>]
    member this.D7P1() = Day07.part1 data7
    [<Benchmark>]
    member this.D7P2() = Day07.part2 data7

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<AOCPerformance>() |> printfn "%A"
    0 // return an integer exit code