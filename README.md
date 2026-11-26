Silent Platformer 2D — Evaluación Sumativa 2

1. Objetivo
Desarrollar un prototipo funcional de videojuego de plataformas 2D en Unity 6 (6000.2.3f1) con estética pixel art urbana postapocalíptica, implementando un game loop completo (menú principal, nivel jugable con recolección, condiciones de victoria/derrota y reinicio), física 2D, audio ambiental y efectos de sonido (BGM/SFX), e interfaz de usuario responsiva.

2. Plataforma Objetivo y Justificación
- Plataforma seleccionada: PC Windows (x86_64).
- Justificación técnica y de despliegue: Un plataformas 2D con desplazamiento horizontal y salto preciso requiere respuesta inmediata de entrada mediante teclado (A/D, flechas y Space) y una pantalla amplia (1920 × 1080) que permita visualizar con claridad las plataformas del Tilemap, los coleccionables y las zonas de caída. En términos de despliegue, compilar un ejecutable independiente (.exe) para Windows facilita la distribución directa mediante repositorio sin depender de tiendas móviles ni emuladores externos, alineándose con un público objetivo de jugadores de PC acostumbrados a controles clásicos de precisión.

3. Configuración Clave y Decisiones Técnicas
- Motor y Pipeline Gráfico: Unity 6000.2.3f1 configurado con Universal Render Pipeline (URP 2D) y Quality Settings equilibrados para renderizado 2D fluido sin sobrecarga de luces volumétricas innecesarias.
- Organización del Proyecto: Estructura modular dentro de /Project/Assets/ separada en Animations/, Audio/, Prefabs/, Scenes/, Scripts/, Sprites/ y Tiles/.
- Física 2D: Detección de contacto con el terreno mediante Physics2D.Raycast contra la capa Capa Suelo, salto por impulso vertical (Rigidbody2D.AddForce en modo Impulse) bajo gravedad 2D, colisiones sólidas en TilemapCollider2D y detección por triggers (OnTriggerEnter2D) para recolección (Moneda) y caída al vacío (ZonaCaida).
- Audio (BGM y SFX): Pista ambiental continua en bucle (bgm_ambiente en AudioManager) separada de los efectos puntuales (sfx_salto, sfx_moneda e interacción UI) reproducidos mediante AudioSource.PlayOneShot() para evitar cortes de sonido.
- Buenas Prácticas: Uso de prefabs reutilizables para coleccionables, parámetros ajustables desde el Inspector ([SerializeField]), control de estado mediante patrón Singleton en GameManager y transición de escenas con SceneManager.

4. Dependencias
- Unity Engine: 6000.2.3f1 (módulo Windows Build Support x86_64).
- Paquetes internos de Unity: Universal RP (com.unity.render-pipelines.universal), TextMeshPro (com.unity.ugui), 2D Tilemap Editor (com.unity.2d.tilemap) y 2D Sprite (com.unity.2d.sprite).

5. Instrucciones de Ejecución y Controles
- Ejecución directa (Build): Descargar el repositorio, abrir la carpeta /Build y ejecutar SilentPlatformer2D.exe (manteniendo en la misma carpeta _Data, MonoBleedingEdge y UnityPlayer.dll).
- Ejecución en Editor: Abrir Unity Hub > Add > Add project from disk, seleccionar /Project, abrir Assets/Scenes/MainMenu.unity y presionar Play.
- Controles: A / D o Flecha Izquierda / Flecha Derecha para moverse; Barra Espaciadora (Space) para saltar; clic izquierdo del ratón para navegar por menús e interfaz.

6. Riesgos y Mitigaciones
- Riesgo: Saltos infinitos en el aire al mantener o repetir la tecla de salto.
  Mitigación: Validación estricta de contacto con el suelo mediante Physics2D.Raycast filtrado por LayerMask (Capa Suelo) antes de aplicar fuerza de impulso.
- Riesgo: Bloqueo del juego al pausar (Time.timeScale = 0f) al terminar la partida y volver a jugar.
  Mitigación: Restablecimiento explícito de Time.timeScale = 1f en los métodos ReiniciarNivel() e IrAMenuPrincipal() antes de invocar SceneManager.LoadScene().
- Riesgo: Desincronización del total de objetivos al modificar el nivel.
  Mitigación: Conteo dinámico de hijos activos dentro del contenedor Coleccionables al iniciar la escena (Start()), evitando valores fijos escritos en código.
- Riesgo: Subida de archivos temporales pesados (Library/, Temp/) al repositorio remoto.
  Mitigación: Configuración de .gitignore específico para Unity y verificación previa con git status antes de cada commit.
