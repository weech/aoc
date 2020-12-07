cd(joinpath("julia", "AOC"))
@time run(`julia -e "import Pkg; Pkg.activate(\".\"); Pkg.test()"`)
cd(joinpath("..", "..", "fsharp"))
@time run(`dotnet test`)
cd(joinpath("..", "rust", "aoc"))
@time run(`cargo test`)

