import * as fs from 'fs';

function solution() {
  let input = fs.readFileSync('./21/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');

  let graph = parseInput(input);
  let ancestors = new Set;
  isHumnAncestor('root', graph, ancestors);
  let [leftValue, rightValue] = dfs('root', graph);
  let evaluated = graph;
  graph = parseInput(input);
  let { leftNode, rightNode } = graph.get('root');

  if (ancestors.has(leftNode)) {
    return getMissingValue(leftNode, rightValue, graph, evaluated, ancestors);
  } else {
    return getMissingValue(rightNode, leftValue, graph, evaluated, ancestors);
  }
}

function isHumnAncestor(node, graph, ancestors = new Set) {
  if (node === 'humn') return true;

  let value = graph.get(node);
  if (typeof value === 'object') {
    let { leftNode, rightNode } = value;
    let isLeftAncestor = isHumnAncestor(leftNode, graph, ancestors);
    let isRightAncestor = isHumnAncestor(rightNode, graph, ancestors);

    if (isLeftAncestor) ancestors.add(leftNode);
    if (isRightAncestor) ancestors.add(rightNode);
    return isLeftAncestor || isRightAncestor;
  } else {
    return false;
  }
}

function dfs(node = 'root', graph) {
  let value = graph.get(node);

  if (typeof value === 'object') {
    let { leftNode, rightNode, operator } = value;
    let leftValue = dfs(leftNode, graph);
    let rightValue = dfs(rightNode, graph);

    let result;
    if (node === 'root') {
      return [leftValue, rightValue]
    } else if (operator === '+') {
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

function getMissingValue(node, target, graph, evaluated, ancestors) {
  if (node === 'humn') return target;

  let value = graph.get(node);

  if (typeof value === 'object') {
    let { leftNode, rightNode, operator } = value;

    if (ancestors.has(leftNode)) {
      let rightValue = evaluated.get(rightNode);

      if (operator === '+') {
        target = target - rightValue;
      } else if (operator === '-') {
        target = rightValue + target;
      } else if (operator === '*') {
        target = target / rightValue;
      } else if (operator === '/') {
        target = target * rightValue;
      }
      return getMissingValue(leftNode, target, graph, evaluated, ancestors);
    } else {
      let leftValue = evaluated.get(leftNode);

      if (operator === '+') {
        target = target - leftValue;
      } else if (operator === '-') {
        target = leftValue - target;
      } else if (operator === '*') {
        target = target / leftValue;
      } else if (operator === '/') {
        target = target * leftValue;
      }
      return getMissingValue(rightNode, target, graph, evaluated, ancestors);
    }
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
