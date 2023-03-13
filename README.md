# ComJamon23
[Download/Descarga](https://juegosasados.itch.io/who-the-crap-is-thiss) 

## Assets we have used from
- [POLYGON Office - Low Poly 3D Art by Synty](https://assetstore.unity.com/packages/3d/props/interior/polygon-office-low-poly-3d-art-by-synty-159492)

- [POLYGON City - Low Poly 3D Art by Synty](https://assetstore.unity.com/packages/3d/environments/urban/polygon-city-low-poly-3d-art-by-synty-95214)

- [Low Poly Shooter Pack - Free Sample](https://assetstore.unity.com/packages/templates/systems/low-poly-shooter-pack-free-sample-144839)

- [Polygonal's Low-Poly Particle Pack](https://assetstore.unity.com/packages/vfx/particles/polygonal-s-low-poly-particle-pack-118355)

- [FREE Skybox Extended Shader](https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400)

# Quién cojones es este? _(Who the crap is this?)_

## Descripción breve:
Un jugador (_Dibujante_) recibirá una descripción física de un Detective, con lo cual tendrá que generar un retrato robot.

El otro jugador (_Detective_) tendrá que buscar una persona que coincida físicamente con la persona dibujada y eliminarla. 


## Mecánica:

Timer interno que se muestra al jugador de la siguiente manera:
* Elementos de música se aceleran
* Voces de gente incrementando
* Tic tac al final

Inicio de partida
* texto rollo superhot explicando partida

Formas de acabar partida
* Por timer
    + Sonido de bocina
    + Zoom al criminal
    + “El criminal ha escapado”
* Objetivo fallido
    + Un segundo de incertidumbre
    + Te miran 
    + Zoom al criminal
    + “Wrong answer” (estilo texto superhot) pum pum
* Objetivo acertado
    + Un segundo de incertidumbre
    + Todo el mundo aplaude + vítores + confetti

* Botón de volver
* “Cambia de rol! Te toca dibujar”
* Se vuelve al menu “Seleccionar id”

**NO hay puntuación**


Al morir el npc, ragdoll

Cola de criminales para no repetir. Se shufflea al principio
* No planteamos persistencia ya que podría ser en web y es un juego de jam
* Si sale uno que ya ha jugado que meta un tiro random y pase a la siguiente

## Transcurso de una partida:
 
Empieza al mismo tiempo para el Dibujante y el Detective. 
La partida termina cuando acaba el vídeo de las indicaciones para dibujar el retrato. Las partidas duran en torno a 1 minuto.

El Detective tendrá un rol específico durante la partida
* Empezará una partida en [...]. 
* Se deja claro el objetivo: matar al NPC del escenario que coincida con el dibujo del Dibujante. Con esto: [...]
* Deberá sincronizar su partida con el * * Dibujante (Posiblemente opcional)
* Comienza la partida → El Detective debe complir su objetivo [...] 
* Si falla:  [...]
* Si acierta: [...]

El Dibujante:
* Empezará la partida al mismo tiempo que el Detective 
* Según escucha la descripción del objetivo, tendrá que dibujar a la persona que se busca [...]
* Objetivo del dibujante claro → Recompensa: [...]

## Entorno jugable:

El espacio jugable para el _Detective_ son una serie de calles y callejones de una ciudad en la que hay muchos personajes diferentes.

El nivel en cada partida es siempre el mismo, lo único que cambia es el objetivo al que hay que matar.

Todos los personajes que hay en el escenario son potenciales objetivos.

## Aplicación móvil:
* Consiste en audios pregrabados
* La pantalla es una mesa con un documento y una radio, barras indicando sonido debajo


![Drag Racing](/ImagesReadme/unnamed.png)



