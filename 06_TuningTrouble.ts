// https://adventofcode.com/2022/day/6
// Day 6: Tuning Trouble

for (const s of inputsDay06())
    for (const n of [4, 14])
        for (let i = n;; ++i)
            if (new Set<string>(s.slice(i-n, i)).size == n) {
                console.log(`last ${n} chars of '${s.slice(0, 5)}...' are unique at ${i}`)
                break
            }

function* inputsDay06() : Generator<string> {
    yield 'mjqjpqmgbljsphdztnvjfqwrcgsmlb'
    yield 'bvwbjplbgvbhsrlpgdmjqwftvncz'
    yield 'nppdvjthqldpwncqszvftbrmjlhg'
    yield 'nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg'
    yield 'zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw'
    yield 'sgrrrrwcrrlqqgppfgfnngsgcgngrrllnqnndzzjgzzzjdjqdjdhhjshjhwwqnnwjnwnjwjttvgvddjrrtvtsvtvqtqhhbchcdhhnwwvqvvsbsqswqqdwdjwwjvvrddgpdpdlpljjwffqnffbllplmmwzwtzzvfzvvjbbmnmppzgzszllsqqpvqvmmzzlccjhchdhlddchdchcddnwdwhhhczzldlsdlssdmmswswzwtwzwjzwzfwwdhwdwjdjldjldlqddhttfbfnbfnfgnfnvnffsszjjsqqdzdsdrsswddggstgsgqgzqgqcqdccqcvcpcspccdgccfflppddqfdfmdffmlflplnppfvvgsgbgtgccmfccfwwthhcjcbbhbwbjbhjjtddrldlrddzjdzdttbfbmmtjmjtjzjvjvgvttthwhhgqhggcbbtqbbqgbqqvccdttfgfwfnwffbfqbfbbnlnzlzbbwnwntwtjwjnjwwsdwwcbbwhwzwhwvhhpwwnvvtvnttgtrrnjjzppmbmfmjffdddvfvjjpfpgpzzwqwpwllwjjzmjmdmdwwdrdttpmmdhdndvvpbpqbbzqzmmtdmmtddlccjvjsjrsscqqzvvbsstccvffcttwrwjjsgsttmgmvvzbbcjbjrbrddvjjnhjhphvhsszqqfrfzfssgfssgddcbbplbbsfsmfmpphssmvvcvrrcgrcgcvggrzggzjjfhjjtpphzppqtptllssbmbrrgrvvhjjnznlnggrnrsnrrphhbqhhhsthsthhhnssmsjjwjppqfqlqqgnnhnmmfsfslfllvnnsdnssgngbbjmjccsrrmjjnljlwlttpffddgbdggmbgbtbzzwgwppczcffvccnssbmmjrrfwwhcwhwqwnqqzsqqjsqqnndqdgqdqgggzjjcvvzdddhnhjnnzlnlwlzlqqvjjpprqrjrfjjrpjjnfntffqtttnjjdbjbdjbbrsrbbmrmccpllmccqrqwqnnsjjmjgjqjpqjqgjjtwwqdqmqtqqmsqmmvlldtllbcctfcccdcddcggmmmsggjddcqddbqqdgqgffststgtftbbdrbrlllnvllcflfpfdffjvvvmdmvvtfvfcvvfgghzzlgzgszzhmzzhfhllgblggfpfzfvzvdzzhsspmmjtjhjggfhggnggbtggqqtztqtmqtqddmzmrmdrmdmqqcbqcqzzvczvzfvvsggcgssjnnjqnntwtmwwhzzzhllqvlljsljjfnnjwjnwnffpggqwwvbwwdbbmbvmvlvnnnppqvqqghqgqppnllhjllvlflfpfhfjjhgjgpjpbjjdpdqdpqdpdwpwffqlffrbrjrtrvtvrtvrtrvtvltlrrvjjlttmtffhvfhfnhnlnfnvvltvlvlbbfllfnllndndcdrrnznssvpvhhhmrmlmhhrnhhpggtftddghhqrqddjttbdbqddpsdppwrwhhhgwhhqrhqrqhhqdhqddjpjqqsdsmddnqncqnqwqdwwhghbhffnsffnsszlssntnbbbfhbhwhzhdzhzbhblbzzzbqzbbgtbbcjbjtjptpwwhlwwhhmshhmbbfjbfflnnlmnnzvnnbtnbbvwvvgcggrzrffwmwhmwhwjwpwwzbbvtbtssdhdlhhdppmmcnmcmffnpfffvbfvfhvhjhffzfbzzfdfpdpzzhbzblbbmffvvcmmttdntnmtmztzbbncbctcqtcqqcvcfcwcdcchphfhjhhjbjnjtjnjwwzsstpprnnhtntvtpthpttpdpzzwcczsscqscsbbmpbbdsdlsslzszjszsczntrqjmmmfqsdwtqqflgsttwfqqvvspnlfvqlrvvbjmmpmttcdnhncmmdfhwwqdrqjqwggrbtgbrdmmrhhvqfvvhsmtfbnthrbltgvdrsbqglgjqtssbvmbjjjbbcgfftgbjmfqzggdtcfzddqlrvwqjjvnmjzjwqrwsqbjgnswpnlbdzdlcvcbqplzgqwmsntzzjhqwfjdprglcccnldfqftgttqbrmclsqtncrjbttcglcvspsgvdjqgrdzzlnhbfqbwnfqcjrrqpprjbqpzhthgsgcflqldsnwsvzgcmfrdvfmqhbcfczhschpwnmdjnjlvrwqllnnhjvjtzhcrqcwlmrqfdhvzcbnvwrgngttwlhcmmgtzwjztscjnmslbvtdrvgdprlfrhggcwtwjhblppfbpljbmwrlwqrfwjwfsftmflsdfrhlvgcbzcvhlhgclvnmtfcqttvcphgvflhdclbmtgsrldgfvtpjcphtzdctrcchwdbdbtpptdnbjnqwdrllmnbcgfltmggpqfbfpmnhcmpgsgptflglzswtmrjfzmwmwphfjngnfmmtqlrsltlvlfmwmjvvtgngllszwzdjjmbnwwgzpqltlrzfdwchgttvlhgjjhjqmlrrwsqlhsgzsgmmsgbgvrlmbprrhlgsjnsdwcbrwvqjqmfcqcwllsvggcznwpzvgpszrqwngcnchvdlrdrgtbsjdqfpsfvwdtdlqwbfjlwrmqbrhwqmfgppwvfbgthnbqnmqqhmpfwbgljcmqqbpnwvztrcrlbvtcnncwwjcbqsmbqnqtrmpwmhlvwtfmsmtpfnmphqdvqfzvmjjhnwdfjnwvmbbwvthhwzjtzzrsmqlqtnnrqjrnchqttgsptfpdcpgfmzvqhwffqmfhwqqbdhmgcrfqtwrcgtgmglmmwhvqwvglfsvwbpvhmnbqhgfgqwwnhdhvnwggsmhjfsjmsrlcvlnhrhrlhbvhdrhbplrzspdmbcnzbwlvcmztwvghlsnzmbnrpssrngpdtmgzfcbqmfdgthcscjspspmcgdmwwwfspgjwzccrfzdpbwrfpgpgzrchffmhvwwppbjwqmdzgtpfmcblzqrghzdbzqzvbnmqbdlzjrwbbhqgtdzntgdbndmndhlnhcvqtlfcrfprfrlfglwvdnszrwjdcmtstcsnvnpcldctvqpcfhjnpvscscrtfqfjcrjlrmcqjfthptbqprbvchjlqzmfcmlhmfmdhhpcqbncmcqjsdmzflwtzfdcgmrbwbcdgjmfhlshsbwmbdcbfbvmqcgwlqpprjfrhzvsjmcjdfnwhcffhtnqpznfzpttsqqwcsvpdhdfbggzpngvbvdlpmvfjjlcfmbvmfqsczprtlnwvqnnlcrdnvpmcbrzvlfgscbcwtrbcpdnpshhmrqmhnwcndptljhwpvtcflqgmzjsfmfdzwwwhnbpzjwzgqmdcdbtfhwtgvcscbdqlcmppwjgghvrmqpwfbnjfhfcrccfzjvtjsjcsmhncdjlclvhfsvlcjcnpbqqqdjmjdbggmfwswvdjscvgrdbpcrcqtndswgdnznzpwtcdgvcrrqpdcpbmbdjrsgnfvgwpgpzttfmsczcmjvhmdpbpmjjcjsvbvbwjpwtwpsdddlsnvrshqvmwsjwwvqnczzljjfptcszgpndgczprbvjbnqpwgzmnlhvbsfbtjnwbtlzqgnmzbmqgqvwzltvqczfpdzfzsfhqlmtfcbfdqtnwzbvqblqmzvmnspntqtqdglrdmdntrghwvpfrbjgpzvrnppvnvfgwdzlvhtcscclbtftlvsprwhjvjlhrhfdgzbfbfphzbhtfdlpzcshhfzhtdvggnnbqvnrwvnhvgjgjpcrztqjmtzlzlrlmndfvctzjdpnmlgmsppqdrzmptvrsptvmmbvbwvhwptrtlfdqdqwfgldtbhqdhszcmwqnhswrdhgmgvbvbhwhlpcflsrwlvsvhvctmwwhtlgmshdqflwsdjbbzgbvbwpfncgqjzfjvmzzhgdzjvghtrtsmwgzpdrngwdbtfzrqsgdmwtdhsftfqcnmjtrqqwthcbgtmqnjvjzzplrzllnjqddvbwnglhtzljwjvscdfdnsvmrgwhjrhlrqpqgmzstnwwjpddhdbsnnsqvtsdhtmfdmbcpzwqmbhhjhcfzbvvglhfdltrmbstjhsqrbs'
}