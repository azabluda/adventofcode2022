﻿// Day 8: Treetop Tree House
// https://adventofcode.com/2022/day/8

let rot90 M =
    let h, w = Array2D.length1 M, Array2D.length2 M
    Array2D.init w h (fun i j -> M.[h - j - 1, i])

let mapRow mapper (M: _[,]) i =
    (0, [])
    |> Seq.unfold (fun (j, mono) ->
        let rec purge mono =
            match mono with
            | head :: tail -> choose head tail
            | empty -> empty
        and choose head tail =
            match M.[i, j] > M.[i, head] with
            | true -> purge tail
            | false -> head :: tail
        let pmono = purge mono
        let ret = mapper j (List.tryHead pmono)
        Some(ret, (j + 1, j :: pmono)))
    |> Seq.take (M.GetLength(1))

let mapMatrix mapper M =
    array2D <| Seq.init (Array2D.length1 M) (mapRow mapper M)

let tth mapper op grid =
    grid
    |> Seq.unfold (fun M -> Some (M, rot90 M))
    |> Seq.take 4
    |> Seq.map (mapMatrix mapper)
    |> Seq.reduce (fun A B ->
        rot90 A
        |> Array2D.mapi (fun i j a -> op a B.[i, j]))
    |> Seq.cast

let tthouse (data: string) =
    let strs = data.Split "\r\n"
    let h, w = strs.Length, strs.[0].Length
    let grid = Array2D.init h w (fun i j -> int strs.[i].[j..j])
    let res1 = grid
               |> tth (fun _ h -> h.IsNone) (||)
               |> Seq.filter id
               |> Seq.length
    let res2 = grid
               |> tth (fun i h -> i - (Option.defaultValue 0 h)) (*)
               |> Seq.max
    [res1; res2]


let inputdata = [
    """30373
25512
65332
33549
35390""";

    """231123311143433334111323513140220312430352020530355453134243354254345525504344520412234022441201231
002313303032210031340314312003010320224404022622605552013026333142351022440040305400004242132311121
220330201413404422305112240330213320021136124315415335033123035640315511030034514131440424021313030
201320204042120231444423450514444264034132022532310222544264033660224245455501245254224301113033202
100230132320401153153211332210134066253434230603403642463504006006105221540233444530342314043014303
122032442431414332522045441015500404344203644233226634442443603306655031345051541032504421424133033
202321031434355013211523304504664140162101106663501334114500001506012426201040202213400414410420443
033422404010423322510522632054006500415024100365442766531456202021112062035410413353104310042243312
110443431341401301233403206566150220000024461521454711652314543050111414550422244110103411011111044
201203422242133202032302263406105000637616447267624727521265126530162241350166442154043253234041313
104022033221031042241034106164466623536612244376774564121253574772146361562004241421223514023220400
440414310500545052512332656122163241446422624364551456541317446567332305362006013160044233035333032
210004115150024101145654165555423566515754711264245263541734173357434433263303155546120144222240002
030042432414255562126315563211647645153362721512142763677766742114561322465216641361020150545534412
403123015321050535251565357633562474213343726145764737476652716513162657627334001426643231115312122
114301034250403323442564327645166772164175757584482225242282452331322551462150315355210411522315334
342135502250325230031455573651146635678748778856468367545852536126334772633520613166345055125343121
240331414023533433311117124527727134563268363765626876836874354585566662473377025500230041031012433
435521420116206160651166217552425745555244872556778684862854644355351636153154772201634233245254141
430142200342652562361156566471575782662428747225633244384252643488668173133776753050305455521012404
101154055404461145474553357357435226287655284867536556382736847785686582546336527411161654600510522
125205523012146541452325155713277563445585483226638265875362343584672325666362655372014353034500103
152205531442652200416553617228567268556458664338963688659762235834558825512465454776303604222502325
233343103643212413761233124526836322436345686967999467774633437656482385721431646313026222325053512
501320340461623523537621263368458686388686939337494776585685787477573454446327641637323555161002134
515322360042621777412121437723477582693859378967469987843385364968522276542363545157536002411505525
205513414500467712163114548784778857594533965478846483595953669558926378465254653724652136460101315
041100604001336362642258742846457779439797634646658538538454373556384447328632577227354404322115241
330432606365061456254442526383759337794885884574763495378836935647477758875783835723357120346646344
033216032220115616576773874887677934937366554498565984797377599998564672852636275344552444234312443
215111363343756276436445667785966574594365559764868559544885789668635945537522848776576443061535203
415106311514725425418676378769957879764874855455454784746657855964963336748226643736534746104406622
152263045613177665648375528588468936695497559865474888988744766995756979628325554634164515561435210
311405136106436751167488647478374739958766847885898896654578454764947876647224765542171351625331402
131443421361717252678663268356687689566784598894969544588495896879354855698683874233631212742011640
114520223332777465223364875648649544757874847599865776469577589798756533866786726347542316613434561
156464406534124114847235838367885658894975444878866689568885867449774994866957448867553672323050532
341262503143467744668835539889446596657688869875588789855844648987585544678955565272321116365045416
354062244564521725625863867798447588968959666555598779795685694965878538389768366485677142523000201
035003352561512182538484597854465568656549866688667696565599674549859567576467434286677737322650166
113555506117316553752425399466445855596576885595765599877985868784676496886647746336332614245114064
260525047433675637362744744746965596745875587686787655888585764566965659969486384433847371754462664
213036656715765765336849567376558777645985876768858575697557669689449869597695848484552253325663114
225226243535142385386537866646748879665589678668868867676957765864677746465394355566587567413633214
652500055731517563452799369639476546488879888978888969876696578989794546483797652865282261755565063
024003237232476584354267844688644488895875886897676966666978587554448587637653957856324154444551030
211124217521676866657866967469554969776798799797689778796897569888647694545836942562787275723446131
510442553712443855247736378387867488559767587798878667777576886666856475837476734748582622223223501
220055237372748256763577645667969449698885959788976987676895869577946945433688788847875644234514060
050143342621224332656354344358485996676998697799776866879788595858756757845396823522835523664231326
316046134444356827463638837499878598587857688887687679866679788667679468887353632246342771363423252
641355624314462855644343484387958776657888786678877699879886989985667894575797876524556757272165011
423566165222552367487595663974765677799955758979788776886679966565888579769359357224677534537141651
240143127214453852854697498688544489889658768667779776696897657765756684836674633574744751223604160
104532363252255383665298956339597974787695766667666978979989695967556679858735728553776467644562216
160252112156645687864737579876579556659787688786899869685678598778546447383854634524285537422561544
144631662137632684444797985884675589666896557778686979865796777678645854558456577835321115175636202
444031263653662743278288954355878959587987677987886889875889867959766986673565324856524455513626620
452205301637437747724748457543645895655589955779998675577599655454594858884369778248235144135565401
234535635453237468746769753649999785595567569977657769578765567659965434939874344677212223116135166
444105464456237738335639358986997765965755775795789756667588966656447534349493346336265231236016106
503403266731262767562768396436564565885788896959978587865898986564897945435443888864226345570560536
103660362732546153332236469356634769484684955899875998959669449899894843593367345252531154430031141
433313341743147116622563299974444465696459985877785895698975778574955544587934636354374541670342234
104336613227653354433482854646447388478769484788767768848459746995748858889722867652664746756650532
124455315366571475275687734947899445845697785949895887444748948474379964335877536646374747202233561
122315236055236642327253763779969358698655997694647598666594646559644756476585247722143334262161245
345552103454725471248265478834399638774649598795666886868748785677755789387875583563314137065031002
410322332227162535482236366633968648797455484884875946656868448359466347548777783274637756643120140
403311304541576275564322776478376978759955444596858985968559893674539657357256874326242333044431503
343563440503243435458363728735763359357947785769684969489758498534634678545673786613447310065143224
113152516545452341611442573535284843994635866487445768694794596986745755247432267662136265543340113
154353641300537551455688753472457857675989577763574873858487488936987572523488633414765434041263333
242422354645546262556563346488438493576836888373556749869537946778585772522787163331611051133204335
303235665440603551575424253236433534846667769999767585833386583775345834273536341323426263551352510
053030406033265327572256285227386337843368688683848398367483486368775387544552557427526262124232115
102311254415365421253455216342557452425939763556975494953887935476865466466552773335155141256501043
544415223520360203126673345687523838428633677773987658675364824355522532266552771672433466302033554
444445546303415444242143251255326422583427368356749366396356862636866376422762775321530066462133350
335014151363326422315426733476578288542533458836887285533354524248552822114455466545010246505051525
025113253162211635452547563652488548824353262345325252722658275255427883666252736546213552414312322
403352135452042501437331142677262273453326783245758487233657647242354557216175336246362031212324354
334512051451011330411642277436174585456448264622345773745358252527373173277211514531553566055210450
443525552450034602520313313415545646578283527627247267654435357633251644633137546562565211320440433
144442150012541262552434267445764164122522886624765246373687767154314415153110143502445150443431031
202425401220411403326146273315422521647643672468433378637542765356644551525410533164460005203522121
440445442434522260350134113673244167326723517373484263521722456627265561124205112001040324024302200
133241444214340263233345225362331635776735317726464122445365756467666216601624122203641551304332133
212333420124003410012341433556217433677653363453333271575214145154125371146442146630233421151232024
324240103540013240405025100243405535224233412432636577232175773454226631111265416431321002353231404
330042444125504323431136353162410146747642365212471233563311373471322005154000234515441441033000211
311222123304343401241325355353252011244755553111776513357461242252101665160165003031522555503110121
222423234211032113215030002041036266501244776723164247523537502152150623662614404111432355402044330
120223310302022255244105120215044423010054135641724271263340546024016502036452454514514012131010434
001000231212004145205543502526310535133132351322613330166226622135343154016155251111332512413400430
300023321142325420044420540430554235664134336551105200015263030025634562522251053500542342334114401
020110224214134125154505403134561022633204410210043364116436221455124661035142022431003221032202111
110333213443142104354013214103233265610323413403454633321354646456612411442532034115333321011400012
202023244334034212011253451025324523366442241613146204642341061310011354032001004431232121344412112"""]

System.Console.WriteLine(inputdata |> List.map tthouse)
