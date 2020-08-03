# RouletteApi
---------------------------------------------
CLEAN CODE API PROJECT
CREATED BY: PAOLA ANDREA GARCIA PRECIADO
---------------------------------------------
IMPORTANT: It's important to install Docker and redis for the project

Localization Project:
https://github.com/pgarciap/RouletteApi.git
Steps to testing
1 - Metod Post with route api/createRoulette -> Input none -> Output string IdRoulete
2 - Metod Put with route api/StartRoulette  -> Input [FromQuery]  string IdRoulette -> Output string State
3 - Metod POST with route api/CreateBet/{userId} -> input string UserId Header and [FromBody] Bet model -> Output string Result
4 - Metod GET with route api/CloseBet/ -> Input [FromQuery] string IdRoulette -> Output List<BetResult> ListBetFromCache
5 - Metod GET with route api/ListRouletteCreate/ -> Input none ->  Output List<BetResult> ListBetFromCache
