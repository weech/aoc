use aoc;
use aoc::parsers;
use criterion::{criterion_group, criterion_main, Criterion};

pub fn criterion_benchmark(c: &mut Criterion) {
    let data1 = parsers::day1();
    c.bench_function("d1p1", |b| b.iter(|| aoc::day1::part1(&data1)));
    c.bench_function("d1p2", |b| b.iter(|| aoc::day1::part2(&data1)));

    let data2 = parsers::day2();
    c.bench_function("d2p1", |b| b.iter(|| aoc::day2::part1(&data2)));
    c.bench_function("d2p2", |b| b.iter(|| aoc::day2::part2(&data2)));

    let data3 = parsers::day3();
    c.bench_function("d3p1", |b| b.iter(|| aoc::day3::part1(&data3)));
    c.bench_function("d3p2", |b| b.iter(|| aoc::day3::part2(&data3)));

    let data4 = parsers::day4();
    c.bench_function("d4p1", |b| b.iter(|| aoc::day4::part1(&data4)));
    c.bench_function("d4p2", |b| b.iter(|| aoc::day4::part2(&data4)));

    let data5 = parsers::day5();
    c.bench_function("d5p1", |b| b.iter(|| aoc::day5::part1(&data5)));
    c.bench_function("d5p2", |b| b.iter(|| aoc::day5::part2(&data5)));

    let data6 = parsers::day6();
    c.bench_function("d6p1", |b| b.iter(|| aoc::day6::part1(&data6)));
    c.bench_function("d6p2", |b| b.iter(|| aoc::day6::part2(&data6)));

    let data7 = parsers::day7();
    c.bench_function("d7p1", |b| b.iter(|| aoc::day7::part1(&data7)));
    c.bench_function("d7p2", |b| b.iter(|| aoc::day7::part2(&data7)));
}

criterion_group!(benches, criterion_benchmark);
criterion_main!(benches);
