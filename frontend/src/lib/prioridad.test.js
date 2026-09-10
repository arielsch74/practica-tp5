import { describe, expect, it } from 'vitest'
import { prioridadDe } from './tareas.js'

describe('prioridadDe', () => {
  const ahora = new Date('2026-09-09T12:00:00Z')

  it('devuelve sin-fecha cuando la tarea no tiene fecha', () => {
    expect(prioridadDe(null, ahora)).toBe('sin-fecha')
    expect(prioridadDe({}, ahora)).toBe('sin-fecha')
  })

  it('devuelve nueva para una tarea de hoy', () => {
    expect(prioridadDe({ creadaEl: '2026-09-09T06:00:00Z' }, ahora)).toBe('nueva')
  })

  it('devuelve esta-semana entre uno y siete días', () => {
    expect(prioridadDe({ creadaEl: '2026-09-06T12:00:00Z' }, ahora)).toBe('esta-semana')
  })

  it('devuelve este-mes entre siete y treinta días', () => {
    expect(prioridadDe({ creadaEl: '2026-08-25T12:00:00Z' }, ahora)).toBe('este-mes')
  })

  it('devuelve vieja pasados los treinta días', () => {
    expect(prioridadDe({ creadaEl: '2026-06-01T12:00:00Z' }, ahora)).toBe('vieja')
  })
})
