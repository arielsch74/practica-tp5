import { describe, expect, it, vi } from 'vitest'
import { LARGO_MAXIMO, ordenarTareas, pendientesDe, validarTitulo } from './tareas.js'

describe('validarTitulo', () => {
  it('acepta un título válido y lo normaliza con trim', () => {
    const resultado = validarTitulo('  Estudiar para la defensa  ')
    expect(resultado).toEqual({ valido: true, titulo: 'Estudiar para la defensa' })
  })

//   it('rechaza títulos vacíos o de solo espacios', () => {
//     expect(validarTitulo('').valido).toBe(false)
//     expect(validarTitulo('   ').valido).toBe(false)
//     expect(validarTitulo(null).valido).toBe(false)
//   })


  it('rechaza títulos que superan el largo máximo', () => {
    const resultado = validarTitulo('a'.repeat(LARGO_MAXIMO + 1))
    expect(resultado.valido).toBe(false)
    expect(resultado.error).toContain(String(LARGO_MAXIMO))
  })
})

describe('ordenarTareas', () => {
  it('ordena de más nueva a más vieja sin mutar el original', () => {
    const tareas = [
      { id: 1, titulo: 'vieja', creadaEl: '2026-07-01T10:00:00Z' },
      { id: 3, titulo: 'nueva', creadaEl: '2026-07-20T10:00:00Z' },
      { id: 2, titulo: 'media', creadaEl: '2026-07-10T10:00:00Z' },
    ]
    const ordenadas = ordenarTareas(tareas)
    expect(ordenadas.map((t) => t.titulo)).toEqual(['nueva', 'media', 'vieja'])
    expect(tareas[0].titulo).toBe('vieja')
  })
})

describe('pendientesDe', () => {
  it('devuelve sólo las tareas que no están hechas', async () => {
    // el impostor: contesta lo que yo quiera, sin red y sin backend
    const traer = vi.fn().mockResolvedValue([
      { id: 1, titulo: 'hecha', hecha: true },
      { id: 2, titulo: 'una', hecha: false },
      { id: 3, titulo: 'otra', hecha: false },
    ])
    const pendientes = await pendientesDe('ana', traer)
    expect(pendientes.map((t) => t.titulo)).toEqual(['una', 'otra'])
  })

  it('le pide a la API la ruta del usuario', async () => {
    const traer = vi.fn().mockResolvedValue([])
    await pendientesDe('ana', traer)
    // el assert que no mira el valor devuelto: mira QUÉ le pediste
    expect(traer).toHaveBeenCalledWith('/api/tareas?u=ana')
  })

  it('devuelve una lista vacía si la API no trae nada', async () => {
    const traer = vi.fn().mockResolvedValue([])
    expect(await pendientesDe('ana', traer)).toEqual([])
  })
})
