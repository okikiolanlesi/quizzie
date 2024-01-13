import { Button } from '@/components/ui/button'
export default function Instruction(){
    return(
        <section className="w-screen mt-2 bg-[#1B223F] ">
            <div className="flex flex-1 justify-start items-center flex-col gap-6  cointainer mx-auto border-b-2 px-6 py-2 h-15 bg-[#ffffff] ">
          
    
    
            <h5 className="font-bold text-center text-4xl">QUIZ DESCRIPTION</h5>
            </div>
            <div className="w-screen flex justify-center items-center gap-6">
                <img className="w-screen px-20"
                src="https://img.freepik.com/free-photo/stock-market-exchange-economics-investment-graph_53876-167143.jpg?size=626&ext=jpg&ga=GA1.2.212456353.1696925456&semt=ais" 
                alt="">
                 </img>
            </div>
            <div>
                <h5 className="text-xl font-medium text-center mt-6 text-gray-50">GENERAL FINANCIAL QUESTIONS</h5>
                <p className="text-center font-light text-gray-50">Questions on stock market,investment,FinancialAccounting and banking</p>
                <p className="text-center font-light text-gray-50"> Total Time: 20 Minutes</p>
                <p className="text-center font-light text-gray-50"> Total Questions: 20 Questionss</p>
                <h5 className="font-bold text-xl text-center mt-6 text-gray-50">INSTRUCTION</h5>
                <p className="px-20 text-gray-50">Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual lockup.
Check out our new font generator and level up your social bios. Need more? Head over to Glyph for all the and you could ever imagine. And don’t forget, we have and the  to all flavors of lorem gypsum. Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual lockup. Check out our new font generator and level up your 

social bios. Need more? Head over to Glyp.for all the and you could ever imagine. And don’t forget, we have and the  to all flavors of lorem gypsum. Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts and visual lockup.Check out our new font generator and level up your social bios. Need more? Head over to Glyph for all the and you could ever imagine. And don’t forget, we have and the  to all flavors of lorem gypsum.
</p>
<button className="border-white bg-white hover:bg-[#1B223F] hover:text-white border-2 p-2 rounded-md justify-center w-5/6 px-100 font-bold text-blue w-full">START QUIZ 
</button>
            </div>

        
        </section>
    )
}