module AOC.Day15

open System.Collections.Generic
open System

let getLinear nx (row, col) = row * nx + col

let checkValidIndex ny nx (testY, testX) =
    0 <= testY
    && testY < ny
    && 0 <= testX
    && testX < nx

let getNeighborIndices verifier (y, x) (ny, nx) =
    let neighbors = [| (0, 1); (0, -1); (1, 0); (-1, 0) |]

    neighbors
    |> Seq.map (fun (offY, offX) -> (offY + y, offX + x))
    |> Seq.filter (verifier ny nx)

let getValue (data: int []) (ny, nx) idx =
    let linear = getLinear nx idx
    data.[linear]

let generatePoints (ny, nx) =
    seq {
        for y in 0 .. ny - 1 do
            for x in 0 .. nx - 1 -> (y, x)
    }

let makeEndPoint (ny, nx) = (ny - 1, nx - 1)

let makeUnvisited locations = locations |> Set.ofSeq

let generateDistances locations =
    locations
    |> Seq.map (fun p -> (p, Int32.MaxValue))
    |> Map.ofSeq

let rec drivingLoop data shape indexer verifier (queue: PriorityQueue<_, _>) endPoint (distances: Map<_, _>) =
    if queue.Count <> 0 then
        let (cost, position) = queue.Dequeue()

        if position = endPoint then
            cost
        else if cost > distances.[position] then
            drivingLoop data shape indexer verifier queue endPoint distances
        else
            let newDistances =
                getNeighborIndices verifier position shape
                |> Seq.fold
                    (fun state neighbor ->
                        let totalValue = cost + (indexer data shape neighbor)

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
                        else
                            state)
                    distances

            drivingLoop data shape indexer verifier queue endPoint newDistances
    else
        distances.[endPoint] // Shouldn't happen

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

let part1 data =
    let (dataArray, shape) = arrayifyStringList data

    let initialDists =
        shape |> generatePoints |> generateDistances

    let queue = initializeQueue (Map.count initialDists)
    let endPoint = makeEndPoint shape
    drivingLoop dataArray shape getValue checkValidIndex queue endPoint initialDists

let generatePoints2 (ny, nx) =
    seq {
        for y in 0 .. ny * 5 - 1 do
            for x in 0 .. nx * 5 - 1 -> (y, x)
    }

let makeEndPoint2 (ny, nx) = (ny * 5 - 1, nx * 5 - 1)

let getValue2 (data: int []) (ny, nx) (y, x) =
    let (pageDown, trueY) = (y / ny, y % ny)
    let (pageRight, trueX) = (x / nx, x % nx)
    let linear = getLinear nx (trueY, trueX)
    let intermediate = data.[linear] + pageDown + pageRight
    // Can't just do mod because 10 % 10 is 0 not 1 and 9 % 9 is 0 not 9
    if intermediate > 9 then
        intermediate % 9
    else
        intermediate

let checkValidIndex2 ny nx (testY, testX) =
    0 <= testY
    && testY < ny * 5
    && 0 <= testX
    && testX < nx * 5

let part2 data =
    let (dataArray, shape) = arrayifyStringList data

    let initialDists =
        shape |> generatePoints2 |> generateDistances

    let queue = initializeQueue (Map.count initialDists)
    let endPoint = makeEndPoint2 shape
    drivingLoop dataArray shape getValue2 checkValidIndex2 queue endPoint initialDists
