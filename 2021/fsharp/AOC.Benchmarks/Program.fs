open System
open AOC
open BenchmarkDotNet.Attributes 
open BenchmarkDotNet.Running 
open System.Collections.Generic
// Very messy from hacking through Benchmarking with a time crunch
module Test = 

    let getLinear nx (row, col) = row * nx + col

    let checkValidIndex ny nx (testY, testX) =
        0 <= testY
        && testY < ny
        && 0 <= testX
        && testX < nx

    let getNeighborIndices (y, x) (ny, nx) =
        let neighbors = [| (0, 1); (0, -1); (1, 0); (-1, 0) |]

        neighbors
        |> Seq.map (fun (offY, offX) -> (offY + y, offX + x))
        |> Seq.filter (checkValidIndex ny nx)

    type Node = {Visited: bool; Score: int}

    let buildQueue locations =
        let mutable queue = PriorityQueue(Seq.length locations)
        locations
        |> Seq.tail // Skip the source
        |> Seq.iter (fun loc -> queue.Enqueue(loc, 9999))
        queue

    let updatePriority location priorty queue = 
        queue 
        |> Seq.filter ((<>) location)
        |> Seq.append (Seq.singleton location)

    let insertOrUpdate cost maybeDistance =
        match maybeDistance with
        | Some x -> Some (cost + x)
        | None -> Some cost

    let getValue (data: int []) (ny, nx) idx =
        let linear = getLinear nx idx
        data.[linear]
(*|     Method |         Mean |      Error |     StdDev |
|------------- |-------------:|-----------:|-----------:|
|      GetNext | 4,817.637 us | 20.2857 us | 18.9752 us |
| ConsiderNode |     5.184 us |  0.0074 us |  0.0066 us |
|       Driver |     6.983 us |  0.0148 us |  0.0139 us |
    let considerNode data shape current (distances: Map<_,_>) =
        getNeighborIndices current shape 
        |> Seq.fold (fun state idx ->
            let value = getValue data shape idx
            let totalDistance = value + distances.[current]

            if totalDistance < distances.[idx] then 
                (Map.change idx (fun c -> match c with | Some _ -> totalDistance | None -> None) distances,

            Map.change
                idx
                (fun currVal ->
                    match currVal with
                    | Some x ->
                        if x > totalDistance then
                            Some totalDistance
                        else
                            Some x
                    | None -> Some totalDistance)
                state)
            distances
*)
    let getNext unvisited = Set.minElement unvisited 

    let generateDistances locations = 
        locations 
        |> Seq.map (fun p -> (p, Int32.MaxValue))
        |> Map.ofSeq        

    let rec drivingLoop data shape (queue: PriorityQueue<_,_>) endPoint (distances: Map<_,_>) =
        if queue.Count <> 0 then 
            let (cost, position) = queue.Dequeue()
            if position = endPoint then 
                cost 
            else 
                if cost > distances.[position] then 
                    drivingLoop data shape queue endPoint distances 
                else 
                    let newDistances = 
                        getNeighborIndices position shape 
                        |> Seq.fold (fun state neighbor -> 
                            let totalValue = cost + (getValue data shape neighbor)

                            let next = (totalValue, neighbor)
                            if totalValue < distances.[neighbor] then 
                                queue.Enqueue(next, totalValue)
                                Map.change
                                    neighbor
                                    (fun currVal ->
                                        match currVal with
                                        | Some x -> Some totalValue
                                        | None -> None)
                                    state
                            else state) distances
                                
                    drivingLoop data shape queue endPoint newDistances 
        else 
            distances.[endPoint] // Shouldn't happen
            
    let generatePoints (ny, nx) =
        seq {
            for y in 0 .. ny - 1 do
                for x in 0 .. nx - 1 -> (y, x)
        }

    let makeEndPoint (ny, nx) = (ny - 1, nx - 1)

    let makeUnvisited locations = locations |> Set.ofSeq
    let arrayifyStringList (rows: string list) =
        let ny = List.length rows
        let nx = (List.head rows) |> String.length

        (rows
        |> Seq.fold
            (fun state row ->
                row
                |> Seq.map (int >> (fun x -> x - (int '0')))
                |> Seq.append state)
            Array.empty
        |> Array.ofSeq,
        (ny, nx))

    let initializeQueue (size: int) = 
        let mutable queue = PriorityQueue(size)
        queue.Enqueue((0, (0, 0)), 0)
        queue

let getData15() = 
    "1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581"
            .Split('\n', StringSplitOptions.TrimEntries) |> List.ofArray

type AOCPerformance() = 

    let data1 = Parsers.day01()
    let data2a = Parsers.day02()
    let data2b = Parsers.day02()
    let data3 = Parsers.day03()
    let (turns, boards) = Parsers.day04()
    let data5 = Parsers.day05()
    let data7 = Parsers.day07()
    let data12 = Parsers.day12()
    let data15 = getData15() //Parsers.day15()
    let (dataArray, shape) = Test.arrayifyStringList data15
    let initialDists = shape |> Test.generatePoints |> Test.generateDistances
    let queue = Test.initializeQueue (Map.count initialDists)
    let endPoint = Test.makeEndPoint shape
    
    [<Benchmark>]
    member this.NewMethod() = Test.drivingLoop dataArray shape queue endPoint initialDists |> ignore

    [<Benchmark>]
    member this.OldMethod() = AOC.Day15.part1 data15 |> ignore


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
    (*[<Benchmark>]
    member this.D7P1() = Day07.part1 data7
    [<Benchmark>]
    member this.D7P2() = Day07.part2 data7
    [<Benchmark>]
    member this.D12P1() = Day12.part1 data12
    [<Benchmark>]
    member this.D12P2() = Day12.part2 data12*)

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<AOCPerformance>() |> printfn "%A"
    0 // return an integer exit code