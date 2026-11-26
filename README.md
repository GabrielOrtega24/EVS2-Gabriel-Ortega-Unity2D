Silent Hill 2, prototipo 2D — Evaluación Sumativa 2 (Unity 6000.2.3f1)

Esto es un trabajo de cátedra, no un juego oficial. Silent Hill 2 es una marca de Konami y este prototipo no tiene relación con Konami ni con el juego original. El objetivo fue tomar su atmósfera urbana y de terror psicológico y llevarla a un videojuego de plataformas 2D en pixel art con un ciclo de juego completo.

Qué es

El proyecto es un prototipo funcional de plataformas 2D hecho en Unity 6000.2.3f1. Cuenta con un menú principal, un nivel jugable, seis medallones para recolectar, una zona de caída que termina la partida y un panel final desde el que se puede reintentar o volver al menú. La meta fue cerrar el game loop completo (inicio, juego, victoria/derrota y reinicio), no solo probar el movimiento.

Cómo se juega

El nivel se llama Level1 y tiene seis medallones repartidos por el escenario. Hay que juntarlos todos sin caerse en la ZonaCaida. Si se juntan todos aparece el cartel "¡NIVEL COMPLETADO!"; si el jugador se cae, "HAS CAÍDO - FIN DEL JUEGO". Desde ese panel se puede reintentar o volver al menú. Arriba, el HUD va mostrando "Medallones: X / 6". Se juega con A/D o las flechas para moverse, Space para saltar y clic izquierdo para los menús.

Por dentro

El proyecto está hecho con Unity 6000.2.3f1 y URP 17.2. Utiliza el renderer Universal del template: el juego es 2D mediante sprites y tilemaps, sin luces volumétricas.

Dentro de Assets se organizan las carpetas Audio, Prefabs, Scenes, Scripts, Settings, Sprites, Tiles y TextMesh Pro (junto a TutorialInfo del template). Las animaciones Idle, Run, Jump y Fall están ubicadas dentro de Sprites.

El movimiento horizontal se aplica sobre transform en Update, y el salto usa Rigidbody2D.AddForce con ForceMode2D.Impulse y gravedad 2D. El suelo se detecta con Physics2D.Raycast contra la capa Suelo (índice 6). Las colisiones del escenario usan TilemapCollider2D y los triggers OnTriggerEnter2D gestionan la recolección de monedas y la zona de caída.

El audio tiene una pista ambiental (bgm_ambiente) en el AudioManager, que suena al iniciar el nivel con PlayOnAwake (sin bucle). Los efectos de salto y moneda se disparan con PlayOneShot, y el sonido de los botones reutiliza sfx_moneda.

El GameManager es un singleton. Cuenta las monedas activas de la escena al iniciar con FindObjectsByType y congela el tiempo (Time.timeScale = 0) cuando termina la partida. ReiniciarNivel e IrAMenuPrincipal devuelven Time.timeScale a 1 antes de cargar la escena. La UI usa un CanvasScaler en ScaleWithScreenSize con referencia 1920x1080; el ejecutable arranca en 1024x768 y en pantalla completa toma la resolución nativa del monitor.

Plataforma objetivo y cómo correrlo

La plataforma objetivo es PC Windows (x86_64). Se eligió Windows porque un plataformas 2D requiere respuesta inmediata de teclado para desplazamiento y saltos precisos, además de una pantalla amplia para leer con claridad las plataformas del tilemap y las zonas de caída. A nivel de despliegue, compilar un ejecutable independiente (.exe) permite probar el juego directamente desde el repositorio sin emuladores ni tiendas externas, apuntando a un público de PC habituado a controles clásicos.

El build de Windows está en la carpeta Build y pesa alrededor de 151 MB. Para probarlo, abre esa carpeta y ejecuta EVS2 Gabriel Ortega.exe manteniendo junto a él EVS2 Gabriel Ortega_Data, MonoBleedingEdge, D3D12, DirectML.dll y UnityPlayer.dll (si se mueve el .exe solo, no arranca).

Para abrirlo en el editor, agrega la carpeta Project desde Unity Hub (Add project from disk), abre Assets/Scenes/MainMenu.unity y presiona Play.

Dependencias: Unity 6000.2.3f1 con el módulo Windows Build Support x86_64, URP 17.2, TextMeshPro (incluido en com.unity.ugui 2.0.0), 2D Tilemap 1.0.0 y 2D Sprite 1.0.0.

Riesgos y mitigaciones

- Saltos múltiples en el aire: Se mitigó leyendo la entrada con GetKeyDown y validando el contacto con el suelo mediante un Physics2D.Raycast filtrado contra la capa Suelo (LayerMask) antes de aplicar el impulso de salto. El rayo mide 2,4 unidades frente a la base del collider en -2,373 (margen de 0,027 unidades), por lo que cualquier ajuste al sprite o collider requiere recalibrar distanciaLineaSuelo.
- Bloqueo del juego tras pausar al finalizar la partida: Al mostrar el panel de victoria o derrota se detiene el tiempo (Time.timeScale = 0), por lo que se aseguró restablecer Time.timeScale a 1 dentro de ReiniciarNivel e IrAMenuPrincipal antes de llamar a SceneManager.LoadScene.
- Desajuste en el total de objetivos al editar el nivel: En lugar de fijar un número manual en el código, GameManager detecta y cuenta automáticamente las monedas activas en la escena al iniciar (Start) mediante FindObjectsByType. Si se instancian monedas dinámicamente después de iniciar, habría que recalcular ese total.
- Subida de archivos temporales pesados al repositorio: Se configuró un archivo .gitignore específico para Unity que excluye carpetas autogeneradas como Library, Temp, Logs y UserSettings, manteniendo versionados únicamente el código fuente en Project y el compilado final en Build.
No hay tests automatizados; podría sumar Unity Test Framework.

Por ahora hay un solo nivel y un solo tipo de coleccionable, sin persistencia ni enemigos.

El .gitignore excluye Library, Temp, Logs y UserSettings. El build sí está versionado, por eso el repo pesa lo que pesa.
