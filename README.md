# Szoo

Szoo er **fremtidens** <em>muligvis</em> bedste spil 

<p>I spillet skal man klappe nogle katte i en lille zoo</p>
<p>Problemerne kommer først når spilleren stopper med at klappe deres katte, de kræver kærlighed, og en mangel på det vil gøre dem ondskabsfulde med risiko for milde eksplosioner</p>
<p>Så klap dine katte, og hvis du fejler, må du slappe kattene væk før de eksploderer</p>

# Spillets komponenter
<p><strong>KATTEN (CAT):</strong></p>
<p>Katten i spillet styres gennem scriptet CatWander.cs.
CatWander.cs har en del formål, hvoraf petbaren og kattens bevægelse er de vigtigste elementer.
Katten står altid mellem at være idle eller wander, hvis den wander går den rundt tilfældigt, hvis den er idle, venter den bare på den næste position den skal gå til.
Hvis katten bliver aet af en hånd går dens pealth (pet health) op, og hvis den ikke aktivt bliver aet, går det ned. Hvis dens Pealth rammer 0, instantiere katten en EvilCat og bliver slettet. 
DEN ONDE KAT (EVILCAT):
EvilCat er ligesom den normale kat, men den vokser konstant i dens størrelse og når den bliver for stor, eksploderer den.</p>

<p><strong>MAP:</strong></p>
<p>Vores map er ikke noget specielt, ud over den absurd gode kvalitet det har. Det er 4 planes der virker som vægge med en gammel windows baggrund som materiale, og et grønt gulv.</p>

<p><strong>Score:</strong></p>
<p>Spillets score er baseret på tiden der er gået, og hvorvidt ens katte dør eller ej. Hvis ens kat dør mister man point, og hvis man slapper evilcats før de sprænger tjener man point.</p>

<p><strong>Hånd:</strong></p>
<p>Hånden kører gennem scriptet HandController.cs</p>
<p>Det som håndens formål er i vores spil, er at den skal kunne blive sendt ud ved at man klikker på venstre museklik, også vil den kunne komme tilbage til dens håndled ved at man giver slip på venstre museklik. Men hvis hånden møder en kat på vej tilbage, så vil den stoppe på katten og kunne aet den, sådan at katten kan få mere liv i dens petbar. Hånden skal også kunne have en funktion at hvis der er en kat så vil man også kunne slappe en kat. Hvis der er en normal kat som bliver slappet, så er det dårligt også får man minuspoint. Men hvis det er en EvilCat som bliver slappet så får man pluspoint.
</p>

<p><strong>Animationer:</strong></p>
<p>Spillets animationer styres gennem de komponenter der allerede eksisterer. Det er dog relevant at bemærke at nogle animationer såsom håndens animationer til tider kan virke lidt <em>quirky</em>. De aktiveres i koden for de relevante komponenter, oftes gennem bools, via diverse animation controllers.</p>

<p><strong>Player(him):</strong></p>
<p>Player kan dreje rundt og bevæge sig. Det meste andet styres af armen. Playeren styres vha. en instance, hvilket betyder at der kun kan være en player der kan styres.</p>

# Flowdiagram
<img alt="YAP" src="https://i.imgur.com/Y9r7F3D.png">