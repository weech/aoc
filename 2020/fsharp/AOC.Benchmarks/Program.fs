open System
open AOC
open BenchmarkDotNet.Attributes 
open BenchmarkDotNet.Running 

type AOCPerformance() = 

    let data1 = Parsers.day1()
    let data2a = Parsers.day2()
    let data2b = Parsers.day2()
    let data3 = Parsers.day3()
    let data4 = Parsers.day4()
    let data5a = Parsers.day5()
    let data5b = Parsers.day5()
    let data6 = Parsers.day6()
    let data7a = Parsers.day7()
    let data7b = Parsers.day7()
    let data9 = Parsers.day9()

    [<Benchmark>]
    member this.D1P1() = Day1.part1 data1
    [<Benchmark>]
    member this.D1P2() = Day1.part2 data1

    [<Benchmark>]
    member this.D2P1() = Day2.part1 data2a
    [<Benchmark>]
    member this.D2P2() = Day2.part2 data2b

    [<Benchmark>]
    member this.D3P1() = Day3.part1 data3
    [<Benchmark>]
    member this.D3P2() = Day3.part2 data3

    [<Benchmark>]
    member this.D4P1() = Day4.part1 data4
    [<Benchmark>]
    member this.D4P2() = Day4.part2 data4

    [<Benchmark>]
    member this.D5P1() = Day5.part1 data5a
    [<Benchmark>]
    member this.D5P2() = Day5.part2 data5b

    [<Benchmark>]
    member this.D6P1() = Day6.part1 data6
    [<Benchmark>]
    member this.D6P2() = Day6.part2 data6

    [<Benchmark>]
    member this.D7P1() = Day7.part1 data7a
    [<Benchmark>]
    member this.D7P2() = Day7.part2 data7b

    [<Benchmark>]
    member this.D09P1() = Day9.part1 data9 25
    [<Benchmark>]
    member this.D09P2() = Day9.part2 data9 25

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<AOCPerformance>() |> printfn "%A"
    0 // return an integer exit code