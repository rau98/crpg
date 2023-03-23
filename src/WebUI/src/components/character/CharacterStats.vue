<script setup lang="ts">
import { type CharacterCharacteristics } from '@/models/character';
import { computeSpeedStats } from '@/services/characters-service';

type Rows = 'weight' | 'hp';

const props = withDefaults(
  defineProps<{
    characteristics: CharacterCharacteristics;
    weight: number;
    longestWeaponLength: number;
    healthPoints: number;
    hiddenRows?: Rows[];
  }>(),
  {
    hiddenRows: () => [],
  }
);

const speedStats = computed(() =>
  computeSpeedStats(
    props.characteristics.attributes.strength,
    props.characteristics.skills.athletics,
    props.characteristics.attributes.agility,
    props.weight,
    props.longestWeaponLength
  )
);
</script>

<template>
  <SimpleTableRow :label="$t('character.stats.hp.title')" :value="$n(healthPoints)" />

  <SimpleTableRow
    v-if="!hiddenRows.includes('weight')"
    :label="$t('character.stats.weight.title')"
    :value="$n(weight)"
  />

  <SimpleTableRow
    :label="$t('character.stats.freeWeight.title')"
    :value="
      $n(Math.min(weight, speedStats.freeWeight), 'decimal') +
      '/' +
      $n(speedStats.freeWeight, 'decimal')
    "
    :tooltip="{
      title: $t('character.stats.freeWeight.title'),
      description: $t('character.stats.freeWeight.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.weightReduction.title')"
    :tooltip="{
      title: $t('character.stats.weightReduction.title'),
      description: $t('character.stats.weightReduction.desc'),
    }"
  >
    <!--
    <div>{{ speedStats.weightReductionFactor }}</div>
    <div>{{ (1 - speedStats.weightReductionFactor) * 100 }}</div>
    -->
    <span class="text-status-danger">
      {{ $n(1 - (speedStats.weightReductionFactor + 1), 'percent') }}
    </span>
  </SimpleTableRow>

  <SimpleTableRow
    :label="$t('character.stats.perceivedWeight.title')"
    :value="$n(speedStats.perceivedWeight, 'decimal')"
    :tooltip="{
      title: $t('character.stats.perceivedWeight.title'),
      description: $t('character.stats.perceivedWeight.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.timeToMaxSpeed.title')"
    :value="$n(speedStats.timeToMaxSpeed, 'second')"
    :tooltip="{
      title: $t('character.stats.timeToMaxSpeed.title'),
      description: $t('character.stats.timeToMaxSpeed.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.nakedSpeed.title')"
    :value="$n(speedStats.nakedSpeed, 'decimal')"
    :tooltip="{
      title: $t('character.stats.nakedSpeed.title'),
      description: $t('character.stats.nakedSpeed.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.currentSpeed.title')"
    :value="$n(speedStats.currentSpeed, 'decimal')"
    :tooltip="{
      title: $t('character.stats.currentSpeed.title'),
      description: $t('character.stats.currentSpeed.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.maxWeaponLength.title')"
    :value="$n(speedStats.maxWeaponLength, 'decimal')"
    :tooltip="{
      title: $t('character.stats.maxWeaponLength.title'),
      description: $t('character.stats.maxWeaponLength.desc'),
    }"
  />

  <SimpleTableRow
    :label="$t('character.stats.movementSpeedPenaltyWhenAttacking.title')"
    :tooltip="{
      title: $t('character.stats.movementSpeedPenaltyWhenAttacking.title'),
      description: $t('character.stats.movementSpeedPenaltyWhenAttacking.desc'),
    }"
  >
    <!--
    <div>
      {{ speedStats.movementSpeedPenaltyWhenAttacking }}
    </div>
    <div>
      {{ speedStats.movementSpeedPenaltyWhenAttacking / 100 }}
    </div>
    -->
    <span :class="{ 'text-status-danger': speedStats.movementSpeedPenaltyWhenAttacking !== 0 }">
      {{ $n(speedStats.movementSpeedPenaltyWhenAttacking / 100, 'percent') }}
    </span>
  </SimpleTableRow>
</template>