cd(joinpath("julia", "AOC"))
run(`julia -e "import Pkg; Pkg.activate(\".\"); Pkg.test()"`)
cd(joinpath("..", "..", "fsharp"))
run(`dotnet test`)
cd(joinpath("..", "rust", "aoc"))
run(`cargo test`)

