import { describe, expect, it } from 'vitest'
import { prioridadDe } from './tareas.js'

// Un test por camino: la función declara cinco, así que hacen falta cinco
// entradas que los recorran. Es exactamente la cuenta que la cobertura de
// ramas estaba haciendo cuando el umbral frenó el build.
describe('prioridadDe', () => {
  const ahora = new Date('2026-07-01T12:00:00Z')

  it('devuelve sin-fecha si la tarea no tiene fecha de creación', () => {
    expect(prioridadDe({}, ahora)).toBe('sin-fecha')
  })

  it('devuelve nueva dentro del primer día', () => {
    expect(prioridadDe({ creadaEl: '2026-07-01T06:00:00Z' }, ahora)).toBe('nueva')
  })

  it('devuelve esta-semana antes de los siete días', () => {
    expect(prioridadDe({ creadaEl: '2026-06-28T12:00:00Z' }, ahora)).toBe('esta-semana')
  })

  it('devuelve este-mes antes de los treinta días', () => {
    expect(prioridadDe({ creadaEl: '2026-06-15T12:00:00Z' }, ahora)).toBe('este-mes')
  })

  it('devuelve vieja pasados los treinta días', () => {
    expect(prioridadDe({ creadaEl: '2026-05-01T12:00:00Z' }, ahora)).toBe('vieja')
  })
})
