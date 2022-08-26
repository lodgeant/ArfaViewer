--delete from set_instructions where ref != '7327-1'


delete from set_instructions where ref in 
(
	select ref from set_details where theme='Agents'
)
delete from set_details where theme='Agents'



INSERT INTO SET_INSTRUCTIONS
(REF,DATA)
VALUES
('8967-1','
<Set xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" Ref="8967-1" Description="Gold Tooth''s Getaway">
  <LargePartPositions />
  <StickerPositions />
  <PartList />
  <SubSetList>
    <SubSet Ref="8967-1_1" Description="Gold Tooth''s Getaway" SubSetType="OFFICIAL">
      <BuildInstructions>
        <SubModel Ref="S1" Description="Gold Tooth''s Getaway" LDrawModelType="FINAL_MODEL" SubModelLevel="0" PosX="0" PosY="0" PosZ="0" RotX="0" RotY="0" RotZ="0" State="NOT_COMPLETED">
          <SubModel Ref="S2" Description="Model 1" LDrawModelType="MODEL" SubModelLevel="1" PosX="0" PosY="0" PosZ="0" RotX="0" RotY="0" RotZ="0" State="NOT_COMPLETED">
            <Step PureStepNo="1" StepLevel="1" ModelRotationX="0" ModelRotationY="0" ModelRotationZ="0" StepBook="0" StepPage="0" State="NOT_COMPLETED" />
          </SubModel>
        </SubModel>
      </BuildInstructions>
    </SubSet>
  </SubSetList>
</Set>')