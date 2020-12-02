use std::fs::File;
use std::io::{BufRead, BufReader};
use std::path::PathBuf;

const DATA: [&str; 3] = ["..", "..", "data"];

pub fn day1() -> Vec<i64> {
    let mut path: PathBuf = DATA.iter().collect();
    path.push("day1.txt");
    let file = File::open(&path).unwrap();
    let reader = BufReader::new(file);
    reader
        .lines()
        .map(|x| x.unwrap().parse::<i64>().unwrap())
        .collect()
}
