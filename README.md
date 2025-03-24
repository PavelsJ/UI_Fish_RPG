# UI_Fish_RPG
Project for University assignment. 

## Mantošana:

Player un Fish manto no klases Character (Player.cs, Fish.cs, Character.cs).

Tas ļauj abām klasēm izmantot kopīgās metodes un īpašības, piemēram, Attack(), TakeDamage(), Health, Strength, kā arī UI atjaunošanas interfeisu (UpdateStatsUI()).

## Enkapsulācija:

Character klasē privātie lauki strength un health ir enkapsulēti, un piekļuve tiem notiek caur publiskajiem īpašumiem Strength un Health (Character.cs).

Wave_Manager izmanto privātās mainīgās waveCountText un fishRemainingText, kuras tiek atjauninātas ar privāto metodi UpdateWaveUI() (Wave_Manager.cs).

## Polimorfisms:

Metode Attack() ir pārdefinēta Player un Fish klasēs, lai pievienotu unikālus efektus, piemēram, indi vai dubulto uzbrukumu (Player.cs, Fish.cs).

Abstraktā klase Hook ļauj dažādiem āķu veidiem (DoubleHook, PrecisionHook, AnchorHook) ieviest savas GetDamage() un LureEffect() metožu versijas (Hook.cs, DoubleHook.cs).

## Abstrakcija:

Abstraktā klase Hook apraksta kopīgās metodes visiem āķu tipiem, bet tās neīsteno. Konkrētie āķi īsteno savu loģiku atvasinātajās klasēs (Hook.cs, DoubleHook.cs).

FishData klase abstrahē zivju datus, iekļaujot pasīvās iemaņas, kas ļauj viegli pievienot jaunus zivju veidus, nemainot pamata loģiku (FishData.cs).

## Papilduzdevumi:

Viļņu sistēmas ieviešana: Wave_Manager pārvalda viļņu skaitu un atlikušo zivju daudzumu katrā vilnī (Wave_Manager.cs).

Kritisko trāpījumu un īpašo efektu mehānika tiek realizēta caur āķu sistēmu Hook_Manager un atbilstošajām āķu klasēm (Hook_Manager.cs, Hook.cs).

Dažādās zivju pasīvās spējas, piemēram, inde, agresija un veiklība, pievieno stratēģiskus elementus cīņai (Fish.cs , FishData.cs).