import * as fs from 'fs';

function solution() {
  let input = fs.readFileSync('./21/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  let graph = parseInput(input);

  return dfs();

  function dfs(node = 'root') {
    let value = graph.get(node);

    if (typeof value === 'object') {
      let { leftNode, rightNode, operator } = value;
      let leftValue = dfs(leftNode);
      let rightValue = dfs(rightNode);

      let result;
      if (operator === '+') {
        result = leftValue + rightValue;
      } else if (operator === '-') {
        result = leftValue - rightValue;
      } else if (operator === '*') {
        result = leftValue * rightValue;
      } else if (operator === '/') {
        result = leftValue / rightValue;
      }

      graph.set(node, result);
    }

    return graph.get(node);
  }
}

function parseInput(input) {
  let graph = new Map;

  for (let line of input) {
    let split = line.split(' ');
    let node = split[0].slice(0, split[0].length - 1);

    if (split.length === 2) {
      let value = parseInt(split[1]);
      graph.set(node, value);
    } else {
      let leftNode = split[1];
      let operator = split[2];
      let rightNode = split[3];
      graph.set(node, { leftNode, rightNode, operator });
    }
  }

  return graph;
}

console.log(solution());
