# Credo
Example provided by Credo Bank

აქ მექნება მიმოხილვა მოკლე შინაარსის სახით იმაზე თუ რა გავაკეთე ამ დავალების ფარგლებში. ვიცი რომ ReadMe ფაილები ძირითადად ინგლისურად იწერება, მაგრამ 
it's gonna be showboating at this point :). დავალება არის შესრულებული .NET CORE 3.1 ზე რაც მოცემული იყო ფაილში. აუთენტიფიკაცია ხდება jwt token-ის გამოყენებით. ერთ ერთ ველად
მითითებული იყო პირადი ნომერი და რახან რეალური გამოცდილებიდან გამომდინარე პირადი ნომერი უნიკალურია ავტორიზაცია მისით და პაროლის გამოყენებით კეთდება, ბაზაში შესაბამის ველს
unique შეზღუდვა ადევს. ერთ ერთი claim-ის სახით ინახება პიროვნების ID რომელიც ბაზის primary key არის და შემდგომში გაკეთებულია extension მეთოდი რომელსაც HTTPContext დან მივწვდებით.
გენერირდება jwt რომელიც იყენებს HmacSha256Signature შიფრაციას. რა თქმა უნდა შეიძლებოდა ავტორიზაციის და აუთენტიფიკაციის გამოსაყენებლად Microsoft Identity Server ის გამოყენება სადაც
უკვე გამზადებული ცხრილები არის ბაზისთვის და უფრო მეტი მეთოდია რომლითაც მომხმარებელთან დაკავშირებული ინფორმაციის მანიპულაცია მარტივდება მაგრამ რო არ ჩამეხლართა ჩემით მოვახდინე
მარტივი იმპლიმენტაცია. პაროლი ბაზაში შენახვამდე იჰეშება და ლოგინისას ანჰეში ხდება. Startup.cs - ში სხვადასხვა სახის სერვისების დარეგისტრირება extension მეთოდის სახით ხდება და გამოყენებული
შესაბამისი IInstaller ინტერფეისი, რომელსაც შემდგომ Reflection - ის გამოყენებით ვიყენებ. ასევე გამოყენებულია Reflection IoC კონტეინერების დასარეგისტრირებლად, შესაბამისი
IScoped,ISingleton,ITransient ინტერფეისების გამოყენებით, ის ხდება იმ კლასების მოძიება რომლებზეც ეს ინტერფეის კონტრაქტებია და შემდგომ სერვისის დარეგისტრირება.
ლოგირებისთვის გამოიყენება Serilog ბიბლიოთეკა. ლოგირება ხდება როგორც კონსოლში, ასევე json გაფართოების მქონე ფაილში და ბაზაში რომლისთვის ცალკე ინსტანსია გაკეთებული ან შეიძლება
რომელიმე არსებულში ერთი ცხრილის ჩამატება და იქ დალოგვა. ჩაშენებულია ასევე GlobalExceptionHandler. ნებისმიერი ერრორი რომელიც უშუალოდ აპლიკაციის დონეზე ან უკან მოხდება დააბრუნებს
კონკრეტულად განსაზღვრულ პასუხს, ხოლო ბაზაში და ფაილში დალოგავს რეალურ მიზეზს. გამოყენებულია Repository Pattern, Unit Of work პატერნები. CQRS გამოსაყენებლად და სხვადასხვა
ბაზებთან სამუშაოდ მესიჯ ბროკერის გამოეყენება იქნებოდა საჭირო და ბოლო ტექნიკური საუბრიდან გამომდინარე რაც გვქონდა მაგ თემაზე უფრო მეტ დროს ვუთმობ თუმცა აქ ჩავთვალე საჭიროდ რომ
არ გამეკეთებინა. ასევე წინა საუბრიდან გამომდინარე რომელიც DDD თემას ეხებოდა, რამოდენიმე რესურსს ჩავუჯექი რათა უფრო უკეთ გამეგო პატერნის მნიშვნელობა და შესაბამისად DDD ელემენტები
გამოყენებული მაქვს. მაგალითად მქონდა ისეთი შემთხვევა რომელზეც რაღაცა დრო დავუთმე ფიქრს თუ როგორ უნდა გამეკეთებინა ერთი კონკრეტული ველი სესხის განაცხადის ცხრილში. კონკრეტულად
კი ვალუტის ველს ეხება საქმე. თუ DDD ს მიდგომით შევხედავთ შესაბამის ველს, მაშინ ჩემი აზრით უბრალოდ ერთი ValueObject-ია რომელიც დამოკიდებულია სესხის განაცხადის entity ზე, მაგრამ
ამავდროულად უშუალოდ აპლიკაციის დონეზე და არსიდან გამომდინარე ვალუტა ცალკე მდგომ entity-დ შეიძლება ჩაითვალოს იმიტომ რომ ფართო ცნებაა და მომავალში შეიძლება გამოჩნდეს იმის მოთხოვნა
რომ სხვა ცხრილებმაც გამოიყენენონ და რეფერენსინგისთვის უფრო მარტივად ამ შემთხვევაში ის მეგონა რომ ცალკე ცხრილში გამეტანა. სხვა მხრივ უშუალოდ მთავარ თემას რაც ეხება, შექმნილია
LoanRepository სადაც ხდება ბაზასთან კავშირი, Data ORM სახით გამოვიყენე Dapper. ხდება განაცხადის შეტანა, განაცხადს ავტომატურად გადაგზავნილი სტატუსი ენიჭება. ყველა ექშენი
რომელიც LOAN კონტროლერში ხდება დალოგინებულ უზერზე მუშაობს, ანუ ყველა რექვესტი იფილტრება კონკრეტულ იუზერზე. შესაბამისად იუზერს შეუძლია ნახოს ყველა ის განაცხადი რომელსაც
დამტკიცებული და უარყოფილი სტატუსი არ აქვს, ასევე რედაქტირება. ბევრი საკითხია რომლის უკეთ მოგვარება შეიძლება, ისეთები როგორიცაა მაგალითად ერრორ მესიჯები რომლებიც შეიძლება ცალკე
სახით იდოს რესურსების ფოლდერში, ასევე ექშენების სახელები და ა.შ. სამწუხაროდ რექვესტების ვალიდაცია ვერ მოვახერხე დროს პრობლემის გამო, წინაზეც შუა კვირას ვუთხარი ანნას რომ ვერ მოვრჩებოდი
და პირდაპირ ვამჯობინე გადამეწია თარიღი და ახლაც დღევანდელ დღეზე ვართ შეთანხმებულები რომ დავალებას გადმოვაგზავნი და არ მინდა დროს გადავაცილო. ვალიდაციას შევასრულებდი რომელიმე
Third Party ბიბლიოთეკის დახმარებით, მაგალითად ისეთით როგორიცაა FluentValidation და შესაბამისი Rule-ებს შევქმნიდი ყოველი კლასისთვის და შემდგომ მოვახდენდი ვალიდურობის შემოწმებას.
სხვა მხრივ მთავარ დანიშნულებას ეს აპი აკეთებს :) ბაზისთვის გითჰაბის რეპოში ბაკ ფაილებს ან სკრიპტებს ჩავაგდებ რომ შესაბამისი ინსტანსების თქვენთან აღდგენა შეგეძლოთ.

მადლობა წინასწარ.
