import * as fs from 'fs';

function solution() {
  let input = fs.readFileSync('./21/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');

  // Evaluate all the expressions in the tree, get the mismatched root values
  let evaluated = parseInput(input);
  let [leftValue, rightValue] = evaluate('root', evaluated);

  // Traverse an unprocessed tree to collect all 'humn' ancestors
  let tree = parseInput(input);
  let ancestors = new Set;
  findHumnAncestors('root', tree, ancestors);

  // Follow the tree down the 'humn' ancestry chain, computing the target value at each level
  let { leftNode, rightNode } = tree.get('root');
  if (ancestors.has(leftNode)) {
    return getMissingValue(leftNode, rightValue, tree, evaluated, ancestors);
  } else {
    return getMissingValue(rightNode, leftValue, tree, evaluated, ancestors);
  }
}

function findHumnAncestors(node, tree, ancestors = new Set) {
  if (node === 'humn') return true;

  let value = tree.get(node);
  if (typeof value === 'object') {
    let { leftNode, rightNode } = value;
    let isLeftAncestor = findHumnAncestors(leftNode, tree, ancestors);
    let isRightAncestor = findHumnAncestors(rightNode, tree, ancestors);

    if (isLeftAncestor) ancestors.add(leftNode);
    if (isRightAncestor) ancestors.add(rightNode);

    return isLeftAncestor || isRightAncestor;
  } else {
    return false;
  }
}

function evaluate(node = 'root', tree) {
  let value = tree.get(node);

  if (typeof value === 'object') {
    let { leftNode, rightNode, operator } = value;
    let leftValue = evaluate(leftNode, tree);
    let rightValue = evaluate(rightNode, tree);

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

    tree.set(node, result);
  }

  return tree.get(node);
}

function getMissingValue(node, target, tree, evaluated, ancestors) {
  if (node === 'humn') return target;

  let value = tree.get(node);

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
      return getMissingValue(leftNode, target, tree, evaluated, ancestors);
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
      return getMissingValue(rightNode, target, tree, evaluated, ancestors);
    }
  }
}


function parseInput(input) {
  let tree = new Map;

  for (let line of input) {
    let split = line.split(' ');
    let node = split[0].slice(0, split[0].length - 1);

    if (split.length === 2) {
      let value = parseInt(split[1]);
      tree.set(node, value);
    } else {
      let leftNode = split[1];
      let operator = split[2];
      let rightNode = split[3];
      tree.set(node, { leftNode, rightNode, operator });
    }
  }

  return tree;
}

console.log(solution());
