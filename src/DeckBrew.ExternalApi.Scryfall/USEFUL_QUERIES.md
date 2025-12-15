# Queries Útiles de Scryfall para DeckBrew

## ?? Comandantes

### Por Identidad de Color

```csharp
// Mono-color
var whiteCommanders = "is:commander id:w";
var blueCommanders = "is:commander id:u";
var blackCommanders = "is:commander id:b";
var redCommanders = "is:commander id:r";
var greenCommanders = "is:commander id:g";

// Dual-color (Guilds de Ravnica)
var azoriusCommanders = "is:commander id:wu";      // Blanco/Azul
var dimirCommanders = "is:commander id:ub";        // Azul/Negro
var rakdosCommanders = "is:commander id:br";       // Negro/Rojo
var gruulCommanders = "is:commander id:rg";        // Rojo/Verde
var selesnyadCommanders = "is:commander id:gw";    // Verde/Blanco
var orzhovCommanders = "is:commander id:wb";       // Blanco/Negro
var izzetCommanders = "is:commander id:ur";        // Azul/Rojo
var golgariCommanders = "is:commander id:bg";      // Negro/Verde
var borosCommanders = "is:commander id:rw";        // Rojo/Blanco
var simicCommanders = "is:commander id:gu";        // Verde/Azul

// Tri-color (Shards de Alara)
var bantCommanders = "is:commander id:gwu";        // Verde/Blanco/Azul
var esperCommanders = "is:commander id:wub";       // Blanco/Azul/Negro
var grixisCommanders = "is:commander id:ubr";      // Azul/Negro/Rojo
var jundCommanders = "is:commander id:brg";        // Negro/Rojo/Verde
var nayaCommanders = "is:commander id:rgw";        // Rojo/Verde/Blanco

// Tri-color (Wedges de Tarkir)
var abzanCommanders = "is:commander id:wbg";       // Blanco/Negro/Verde
var jeskaiCommanders = "is:commander id:urw";      // Azul/Rojo/Blanco
var sultaiCommanders = "is:commander id:bgu";      // Negro/Verde/Azul
var marduCommanders = "is:commander id:rwb";       // Rojo/Blanco/Negro
var temurCommanders = "is:commander id:gur";       // Verde/Azul/Rojo

// 4-color
var fourColorNoWhite = "is:commander id:ubrg";
var fourColorNoBlue = "is:commander id:brgw";
var fourColorNoBlack = "is:commander id:rgwu";
var fourColorNoRed = "is:commander id:gwub";
var fourColorNoGreen = "is:commander id:wubr";

// 5-color
var fiveColorCommanders = "is:commander id:wubrg";
```

### Comandantes por Tema

```csharp
// Tribal
var elfCommanders = "is:commander o:elf";
var goblinCommanders = "is:commander o:goblin";
var zombieCommanders = "is:commander o:zombie";
var dragonCommanders = "is:commander o:dragon";
var vampireCommanders = "is:commander o:vampire";
var angelCommanders = "is:commander o:angel";
var demonCommanders = "is:commander o:demon";
var wizardCommanders = "is:commander o:wizard";

// Estrategias
var voltronCommanders = "is:commander (o:equip OR o:aura)";
var tokenCommanders = "is:commander o:\"create\" o:token";
var counterspellCommanders = "is:commander o:counter o:spell";
var graveyard = "is:commander (o:graveyard OR o:\"from your graveyard\")";
var artifactCommanders = "is:commander o:artifact";
var enchantmentCommanders = "is:commander o:enchantment";
var landmattersCommanders = "is:commander o:land";
var spellslingerCommanders = "is:commander o:\"cast\" o:\"instant or sorcery\"";
```

### Comandantes Económicos

```csharp
var budgetCommanders = "is:commander usd<5";
var veryBudgetCommanders = "is:commander usd<1";
var midRangeCommanders = "is:commander usd>=5 usd<=20";
var expensiveCommanders = "is:commander usd>50";
```

## ?? Construcción de Mazos

### Rampa (Aceleración de Maná)

```csharp
// Rampa verde
var greenRamp = "c:g (o:\"search your library for\" o:land) (t:instant OR t:sorcery) f:commander";
var dorks = "c:g t:creature o:\"{T}: Add\" f:commander cmc<=3";

// Artefactos de maná
var manaRocks = "t:artifact o:\"{T}: Add\" f:commander cmc<=3";
var signets = "t:artifact o:\"add\" set:type:signet f:commander";
var talismans = "t:artifact o:\"add\" set:type:talisman f:commander";
```

### Remoción

```csharp
// Remoción de criaturas
var spotRemoval = "(o:destroy o:target o:creature) (t:instant OR t:sorcery) f:commander";
var boardWipes = "(o:destroy o:\"all creatures\") t:sorcery f:commander";

// Remoción de artefactos/encantamientos
var naturalize = "c:g (o:destroy o:target) (o:artifact OR o:enchantment) f:commander";

// Remoción general
var vindicateEffects = "o:\"destroy target permanent\" f:commander";
var exileRemoval = "(o:exile o:target) (t:instant OR t:sorcery) f:commander";
```

### Robo de Cartas

```csharp
// Azul
var blueDraw = "c:u o:draw (t:instant OR t:sorcery OR t:enchantment) f:commander";
var cantrips = "c:u o:\"draw a card\" cmc<=2 f:commander";

// Negro
var blackDraw = "c:b (o:draw OR o:\"card from your graveyard\") f:commander";

// Verde
var greenDraw = "c:g (o:draw o:creature) f:commander";

// Colourless
var colorlessDraw = "c:c o:draw (t:artifact) f:commander";
```

### Protección

```csharp
var counterspells = "c:u o:counter o:spell f:commander";
var hexproofGivers = "o:hexproof f:commander";
var indestructibleGivers = "o:indestructible f:commander";
var protection = "o:protection f:commander";
```

### Tutores (Búsqueda)

```csharp
var vampiricTutor = "c:b o:\"search your library\" o:\"put\" o:hand f:commander";
var creatureTutors = "o:\"search your library for a creature\" f:commander";
var artifactTutors = "o:\"search your library for an artifact\" f:commander";
var enchantmentTutors = "o:\"search your library for an enchantment\" f:commander";
```

## ?? Por Tipo de Carta

### Criaturas

```csharp
// Por estadísticas
var bigCreatures = "t:creature pow>=7 f:commander";
var efficientCreatures = "t:creature pow>=cmc f:commander";
var lowDropCreatures = "t:creature cmc<=2 f:commander";

// Por habilidades
var flyingCreatures = "t:creature o:flying f:commander";
var hastecreatures = "t:creature o:haste f:commander";
var trampleCreatures = "t:creature o:trample f:commander";
var vigilanceCreatures = "t:creature o:vigilance f:commander";
var deathtouchCreatures = "t:creature o:deathtouch f:commander";
var lifelinkCreatures = "t:creature o:lifelink f:commander";
```

### Instant y Sorcery

```csharp
var efficientSpells = "(t:instant OR t:sorcery) cmc<=3 f:commander";
var xSpells = "(t:instant OR t:sorcery) o:\"{X}\" f:commander";
var rituals = "c:r o:\"add\" o:\"{R}\" (t:instant OR t:sorcery) f:commander";
```

### Artefactos

```csharp
var equipments = "t:equipment f:commander";
var vehicles = "t:vehicle f:commander";
var manaArtifacts = "t:artifact o:\"add\" f:commander";
var sacArtifacts = "t:artifact o:\"sacrifice\" f:commander";
```

### Encantamientos

```csharp
var auras = "t:aura f:commander";
var enchantressCards = "t:enchantment o:\"whenever you cast an enchantment\" f:commander";
var sagas = "t:saga f:commander";
```

### Tierras

```csharp
// Dual lands
var dualLands = "t:land (o:\"{T}: Add {W}\" OR o:\"{T}: Add {U}\") f:commander";

// Fetch lands
var fetchLands = "t:land o:\"search your library for\" f:commander";

// Utility lands
var utilityLands = "t:land (o:draw OR o:destroy OR o:create) f:commander";

// Pain/Check/Shock lands
var painLands = "t:land o:\"deals 1 damage to you\" f:commander";
```

## ?? Cartas Específicas Populares

### Staples de Commander

```csharp
// Rampa
var solRing = "!\"Sol Ring\"";
var arcaneSignet = "!\"Arcane Signet\"";
var commandTower = "!\"Command Tower\"";

// Remoción
var swordsToPlowshares = "!\"Swords to Plowshares\"";
var cyclonic Rift = "!\"Cyclonic Rift\"";
var wrathOfGod = "!\"Wrath of God\"";

// Robo
var rhysticStudy = "!\"Rhystic Study\"";
var mysticalTutor = "!\"Mystical Tutor\"";

// Protección
var counterspell = "!\"Counterspell\"";
var swiftfootBoots = "!\"Swiftfoot Boots\"";
var lightningGreaves = "!\"Lightning Greaves\"";
```

## ?? Filtros por Colección

```csharp
// Últimas colecciones (ejemplos)
var bloomburrow = "set:blb";
var outlawsOfThunderJunction = "set:otj";
var murdersAtKarlovManor = "set:mkm";
var lostCaverns = "set:lci";

// Comandantes pre-construidos
var commanderLegends = "set:cmr OR set:cmc";
var commander2021 = "set:c21";
```

## ?? Queries Compuestas Avanzadas

```csharp
// Comandantes económicos con habilidades de robo
var budgetDrawCommanders = "is:commander usd<10 o:draw";

// Criaturas eficientes para aggro
var aggroCreatures = "t:creature cmc<=3 pow>=2 (o:haste OR o:flying) f:commander";

// Lands que entran sin tap en 2-color
var fastDualLands = "t:land id:wu -o:\"enters tapped\" f:commander";

// Instant de protección económicos
var budgetProtection = "t:instant (o:indestructible OR o:hexproof OR o:protection) usd<2 f:commander";

// Artefactos colorless que generan ventaja de cartas
var cardAdvantageArtifacts = "t:artifact c:c (o:draw OR o:\"return\" o:\"from your graveyard\") f:commander";
```

## ?? Tips para Búsquedas

1. **Usar comillas** para texto exacto: `o:"draw a card"`
2. **Operadores lógicos**: `AND`, `OR`, `NOT` o `-`
3. **Paréntesis** para agrupar: `(t:instant OR t:sorcery) c:u`
4. **Comparadores**: `>=`, `<=`, `>`, `<`, `=`, `!=`
5. **Wildcards**: No soportados directamente, usar fuzzy search

## ?? Recursos

- [Sintaxis completa de Scryfall](https://scryfall.com/docs/syntax)
- [Referencia de filtros](https://scryfall.com/docs/reference)
