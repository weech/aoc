open System
open AOC
open BenchmarkDotNet.Attributes 
open BenchmarkDotNet.Running 

type AOCPerformance() = 

    let data1 = Parsers.day01()
    let data2a = Parsers.day02()
    let data2b = Parsers.day02()
    let data3 = Parsers.day03()

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

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<AOCPerformance>() |> printfn "%A"
    0 // return an integer exit code