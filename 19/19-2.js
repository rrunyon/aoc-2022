
import * as fs from 'fs';
import { Queue } from '@datastructures-js/queue';

function solution() {
  let input = fs.readFileSync('./19/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  let blueprints = parseBlueprints(input);

  let geodeCounts = [];

  let currentMax;
  for (let blueprint of blueprints) {
    currentMax = -Infinity;
    geodeCounts.push(dfs(blueprint));
    console.log(geodeCounts);
  }

  return geodeCounts.reduce((a, b) => a * b);

  function dfs(
    blueprint,
    state = {
      minute: 1,
      oreRobots: 1, 
      ore: 0, 
      clayRobots: 0, 
      clay: 0, 
      obsidianRobots: 0, 
      obsidian: 0, 
      geodeRobots: 0, 
      geodes: 0 
    },
    buildingRobots = new Queue,
  ) {

    let {
      oreRobotCost,
      clayRobotCost,
      obsidianRobotCost,
      geodeRobotCost,
    } = blueprint;

    while (buildingRobots.size() && (state.minute - buildingRobots.front().minute) > 1) {
      let robot = buildingRobots.dequeue();
      if (robot.type == 'geode') {
        state.geodeRobots++;
      } else if (robot.type == 'obsidian') {
        state.obsidianRobots++;
      } else if (robot.type == 'clay') {
        state.clayRobots++;
      } else if (robot.type == 'ore') {
        state.oreRobots++;
      }
    }

    state.ore += state.oreRobots;
    state.clay += state.clayRobots;
    state.obsidian += state.obsidianRobots;
    state.geodes += state.geodeRobots;

    let maxAchievable = state.geodes + ((32 - state.minute) * state.geodeRobots);
    for (let i = state.minute; i <= 32; i++) {
      maxAchievable += (32 - i);
    }
    if (maxAchievable < currentMax) return 0;

    if (state.minute >= 32) {
      currentMax = Math.max(currentMax, state.geodes);
      return state.geodes;
    }

    let counts = [];

    if (state.ore >= geodeRobotCost[0] && state.obsidian >= geodeRobotCost[1]) {
      let newState = { ...state, minute: state.minute + 1 };
      newState.ore -= geodeRobotCost[0];
      newState.obsidian -= geodeRobotCost[1];

      let newBuildingRobots = buildingRobots.clone();
      newBuildingRobots.enqueue({ type: 'geode', minute: state.minute });

      counts.push(dfs(blueprint, newState, newBuildingRobots));
    }

    if (state.ore >= obsidianRobotCost[0] && state.clay >= obsidianRobotCost[1] && state.obsidianRobots < blueprint.maxObsidianNeeded) {
      let newState = { ...state, minute: state.minute + 1 };
      newState.ore -= obsidianRobotCost[0];
      newState.clay -= obsidianRobotCost[1];

      let newBuildingRobots = buildingRobots.clone();
      newBuildingRobots.enqueue({ type: 'obsidian', minute: state.minute });

      counts.push(dfs(blueprint, newState, newBuildingRobots));
    }

    if (state.ore >= clayRobotCost && state.clayRobots < blueprint.maxClayNeeded) {
      let newState = { ...state, minute: state.minute + 1 };
      newState.ore -= clayRobotCost;

      let newBuildingRobots = buildingRobots.clone();
      newBuildingRobots.enqueue({ type: 'clay', minute: state.minute });

      counts.push(dfs(blueprint, newState, newBuildingRobots));
    }

    if (state.ore >= oreRobotCost && state.oreRobots < blueprint.maxOreNeeded) {
      let newState = { ...state, minute: state.minute + 1 };
      newState.ore -= oreRobotCost;

      let newBuildingRobots = buildingRobots.clone();
      newBuildingRobots.enqueue({ type: 'ore', minute: state.minute });

      counts.push(dfs(blueprint, newState, newBuildingRobots));
    }

    let newState = { ...state, minute: state.minute + 1 };
    counts.push(dfs(blueprint, newState, buildingRobots.clone()));

    return Math.max(...counts);
  }
}

function parseBlueprints(input) {
  let blueprints = [];

  for (let i = 0; i < input.length; i++) {
    let line = input[i];
    if (!line) continue;

    let split = line.split(' ');
    let numbers = split.map(i => parseInt(i)).filter(i => !isNaN(i));
    let blueprint = {
      index: i,
      oreRobotCost: numbers[1],
      clayRobotCost: numbers[2],
      obsidianRobotCost: [numbers[3], numbers[4]],
      geodeRobotCost: [numbers[5], numbers[6]],
    }
    blueprint.maxOreNeeded = Math.max(blueprint.oreRobotCost, blueprint.clayRobotCost, blueprint.obsidianRobotCost[0], blueprint.geodeRobotCost[0]);
    blueprint.maxClayNeeded = blueprint.obsidianRobotCost[1];
    blueprint.maxObsidianNeeded = blueprint.geodeRobotCost[1];
    blueprints.push(blueprint);
  }

  return blueprints.slice(0, 3);
}

console.log(solution());
