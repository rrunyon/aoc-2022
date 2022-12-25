import * as fs from 'fs';

function solution() {
  let input = fs.readFileSync('./20/test-input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  let array = [];
  let positionMap = new Map;

  for (let i = 0; i < input.length; i++) {
    let element = input[i];
    let number = parseInt(element);
    array.push(number);
    positionMap.set(number, 
    line.push
  }
}

console.log(solution());
